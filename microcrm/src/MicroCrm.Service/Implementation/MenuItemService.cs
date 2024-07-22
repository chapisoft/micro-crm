﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicroCrm.Domain.Models;
using MicroCrm.Dto;
using URF.Core.Abstractions.Trackable;
using URF.Core.Services;

namespace MicroCrm.Service
{
  public class MenuItemService : Service<MenuItem>, IMenuItemService
  {
    private readonly IDataTableImportMappingService _mappingservice;
    public MenuItemService(
      ITrackableRepository<MenuItem> repository,
      IDataTableImportMappingService mappingservice)
        : base(repository)
    {

      this._mappingservice = mappingservice;
    }

    public async Task<IEnumerable<MenuItem>> GetByParentId(int parentid) => await this.Repository.Queryable().Where(x => x.ParentId == parentid).ToListAsync();




    private int getParentIdByTitle(string title)
    {

      var menuitem = this.Queryable().Where(x => x.Title == title).FirstOrDefault();
      if (menuitem == null)
      {
        throw new Exception($"没有找到 {title} 父菜单");
      }
      else
      {
        return menuitem.Id;
      }
    }

    public async Task ImportDataTableAsync(System.Data.DataTable datatable)
    {
      var mapping = await this._mappingservice.Queryable().Where(x => x.EntitySetName == "MenuItem" && ((x.IsEnabled == true) || (x.IsEnabled == false && !(x.DefaultValue == null || x.DefaultValue.Equals(string.Empty))))).ToListAsync();
      foreach (DataRow row in datatable.Rows)
      {
       

        var requiredfield = mapping.Where(x => x.IsRequired == true).FirstOrDefault()?.SourceFieldName;
        if (requiredfield != null && !row.IsNull(requiredfield) && row[requiredfield] != DBNull.Value && Convert.ToString(row[requiredfield]).Trim() != string.Empty)
        {
          var item = new MenuItem();
          foreach (var field in mapping)
          {
            var defval = field.DefaultValue;
            var contation = datatable.Columns.Contains((field.SourceFieldName == null ? "" : field.SourceFieldName));
            if (contation && !row.IsNull(field.SourceFieldName) && row[field.SourceFieldName] != DBNull.Value && row[field.SourceFieldName].ToString() != string.Empty)
            {
              var menuitemtype = item.GetType();
              var propertyInfo = menuitemtype.GetProperty(field.FieldName);
              //关联外键查询获取Id
              switch (field.FieldName)
              {
                case "ParentId":
                  var title = row[field.SourceFieldName].ToString();
                  var parentid = this.getParentIdByTitle(title);
                  propertyInfo.SetValue(item, Convert.ChangeType(parentid, propertyInfo.PropertyType), null);
                  break;
                default:
                  var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                  var safeValue = (row[field.SourceFieldName] == null) ? null : Convert.ChangeType(row[field.SourceFieldName], safetype);
                  propertyInfo.SetValue(item, safeValue, null);
                  break;
              }
            }
            else if (!string.IsNullOrEmpty(defval))
            {
              var menuitemtype = item.GetType();
              PropertyInfo propertyInfo = menuitemtype.GetProperty(field.FieldName);
              if (defval.ToLower() == "now" && propertyInfo.PropertyType == typeof(DateTime))
              {
                propertyInfo.SetValue(item, Convert.ChangeType(DateTime.Now, propertyInfo.PropertyType), null);
              }
              else
              {
                var safetype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var safeValue = Convert.ChangeType(defval, safetype);
                propertyInfo.SetValue(item, safeValue, null);
              }
            }
          }
          this.Insert(item);
        }

      }
    }

    public async Task<Stream> ExportExcelAsync(Expression<Func<MenuItem, bool>> filters, string sort = "Id", string order = "asc")
    {
      var expcolopts = await this._mappingservice.Queryable()
             .Where(x => x.EntitySetName == "DataTableImportMapping" && x.Exportable)
             .Select(x => new ExpColumnOpts()
             {
               EntitySetName = x.EntitySetName,
               FieldName = x.FieldName,
               IsExportable = x.Exportable,
               SourceFieldName = x.SourceFieldName,
               LineNo = x.LineNo
             }).ToArrayAsync();
      var data = await this.Query(filters).OrderBy(n => n.OrderBy($"{sort} {order}")).SelectAsync();
      return await NPOIHelper.ExportExcelAsync("MenuItem", data, expcolopts);

    }

    public async Task<IEnumerable<MenuItem>> CreateWithControllerAsync()
    {
      var list = new List<MenuItem>();

      var asm = Assembly.Load("MicroCrm.WebUI");

      var controlleractionlist = asm.GetTypes()
              .Where(type => type.Name.EndsWith("Controller"))
              .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
              .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
              .Select(x => new {
                Controller = x.DeclaringType.Name,
                Action = x.Name,
                ReturnType = x.ReturnType.Name,
                Attributes =""
                })
              .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
      var i = 1000;
      var filter = new string[] { "Index"};
      foreach (var item in controlleractionlist.OrderBy(x => x.Controller).ThenBy(x => x.Action))
      {
        //var routename = (item.Attributes as RouteAttribute).Name;
        if (!string.IsNullOrEmpty(item.Controller) && 
          !string.IsNullOrEmpty(item.Action) &&
          item.Action=="Index")
        {
          var menu = new MenuItem();
          menu.LineNum = (i++).ToString("0000");
          menu.Description = item.Controller.Replace("Controller", "");
          menu.Title = item.Controller.Replace("Controller", "");
          menu.Url = "/" + item.Controller.Replace("Controller", "") + "/" + item.Action;
          menu.Action = item.Action;
          menu.Controller = item.Controller.Replace("Controller", "");
          menu.IsEnabled = true;
          if (!await this.Queryable().Where(x => x.Url == menu.Url).AnyAsync())
          {
            this.Insert(menu);

          }

          list.Add(menu);
        }
      }

      return list;
    }


    public async Task<IEnumerable<MenuItem>> ReBuildMenusAsync()
    {
      foreach (var item in await this.Queryable().ToListAsync())
      {
        this.Delete(item);
      }
      return await this.CreateWithControllerAsync();
    }
  }
}



