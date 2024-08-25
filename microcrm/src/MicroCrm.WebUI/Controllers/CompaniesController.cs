﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroCrm.Domain.Models;
using MicroCrm.Service;
using MicroCrm.WebUI.Extensions;
using URF.Core.Abstractions;
using System.Linq.Dynamic.Core;

namespace MicroCrm.WebUI.Controllers
{
  public class CompaniesController : Controller
  {
    private  readonly ICompanyService companyService;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<CompaniesController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public CompaniesController(ICompanyService companyService,
          IUnitOfWork unitOfWork,
          IWebHostEnvironment webHostEnvironment,
          ILogger<CompaniesController> logger)
    {
      this.companyService = companyService;
      this.unitOfWork = unitOfWork;
      this._logger = logger;
      this._webHostEnvironment = webHostEnvironment;
    }

    // GET: Companies
    public IActionResult Index()=> View();
    //data source
    public async Task<JsonResult> GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
    {
      try
      {
        var filters = PredicateBuilder.FromFilter<Company>(filterRules);
        var total = await this.companyService
                             .Query(filters)
                             .AsNoTracking()
                             .CountAsync()
                              ;
        var pagerows = (await this.companyService
                             .Query(filters)
                              .AsNoTracking()
                           .OrderBy(n => n.OrderBy($"{sort} {order}"))
                           .Skip((page - 1) * rows).Take(rows)
                           .SelectAsync())
                           .Select(n => new
                           {
                             Id = n.Id,
                             Name = n.Name,
                             FullName = n.FullName,
                             Code = n.Code,
                             Address = n.Address,
                             Contact = n.Contact,
                             PhoneNumber = n.PhoneNumber,
                             PostCode = n.PostCode,
                             Note = n.Note,
                             RegisterDate = n.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"),
                             Private = n.Private,
                             CreatedBy = n.CreatedBy
                           }).ToList();
        var pagelist = new { total = total, rows = pagerows };
        return Json(pagelist);
      }
      catch(Exception e) {
        throw e;
        }

    }
    //Edit 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Edit(Company company)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.companyService.Update(company);

          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result = result });
        }
         catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message });
        }
      }
      else
      {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors });
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(work);
    }
    //新建
    [HttpPost]
    [ValidateAntiForgeryToken]
   
    public async Task<JsonResult> Create([Bind("Name,Code,Address,Contact,PhoneNumber,RegisterDate")] Company company)
    {
      if (ModelState.IsValid)
      {
        try
        {
          this.companyService.Insert(company);
       await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true});
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message });
        }

        //DisplaySuccessMessage("Has update a Work record");
        //return RedirectToAction("Index");
      }
      else
       {
        var modelStateErrors = string.Join(",", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors });
        //DisplayErrorMessage(modelStateErrors);
      }
      //return View(work);
    }
    //Delete current record
    //GET: Companies/Delete/:id
    [HttpGet]
    public async Task<JsonResult> Delete(int id)
    {
      try
      {
        await this.companyService.DeleteAsync(id);
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true });
      }
     
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }
    //Delete the selected records
    [HttpPost]
    public async Task<JsonResult> DeleteChecked(int[] id)
    {
      try
      {
        foreach (var key in id)
        {
          await this.companyService.DeleteAsync(key);
        }
        await this.unitOfWork.SaveChangesAsync();
        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.GetBaseException().Message });
      }
    }
    //Accept datagrid edit data
    [HttpPost]
    public async Task<JsonResult> AcceptChanges(Company[] companies)
    {
      if (ModelState.IsValid)
      {
        try
        {
          foreach (var item in companies)
          {
            this.companyService.ApplyChanges(item);
          }
          var result = await this.unitOfWork.SaveChangesAsync();
          return Json(new { success = true, result });
        }
        catch (Exception e)
        {
          return Json(new { success = false, err = e.GetBaseException().Message });
        }
      }
      else
      {
        var modelStateErrors = string.Join(",", ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors });
      }

    }
    //ExportExcel
    [HttpPost]
    public async Task<IActionResult> ExportExcel(string filterRules = "", string sort = "Id", string order = "asc")
    {
      var fileName = "compnay" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
      var stream = await this.companyService.ExportExcelAsync(filterRules, sort, order);
      return File(stream, "application/vnd.ms-excel", fileName);
    }
    //Importexcel
    [HttpPost]
    public async Task<IActionResult> ImportExcel()
    {
      try
      {
        var watch = new Stopwatch();
        watch.Start();
        var total = 0;
        if (Request.Form.Files.Count > 0)
        {
          for (var i = 0; i < this.Request.Form.Files.Count; i++)
          {
            var model = Request.Form["model"].FirstOrDefault() ?? "company";
            var folder = Request.Form["folder"].FirstOrDefault() ?? "company";
            var autosave = Convert.ToBoolean(Request.Form["autosave"].FirstOrDefault());
            var properties = (Request.Form["properties"].FirstOrDefault()?.Split(','));
            var file = Request.Form.Files[i];
            var filename = file.FileName;
            var contenttype = file.ContentType;
            var size = file.Length;
            var ext = Path.GetExtension(filename);
            var path = Path.Combine(this._webHostEnvironment.ContentRootPath, "UploadFiles", folder);
            if (!Directory.Exists(path))
            {
              Directory.CreateDirectory(path);
            }
            //var datatable = await NPOIHelper.GetDataTableFromExcelAsync(file.OpenReadStream(), ext);
            await this.companyService.ImportDataTableAsync(file.OpenReadStream());
            total= await this.unitOfWork.SaveChangesAsync();
            if (autosave)
            {
              var filepath = Path.Combine(path, filename);
              file.OpenReadStream().Position = 0;

              using (var stream = System.IO.File.Create(filepath))
              {
                await file.CopyToAsync(stream);
              }
            }

          }
        }
        watch.Stop();
        return Json(new { success = true, total = total, elapsedTime = watch.ElapsedMilliseconds });
      }
      catch (Exception e) {
        Response.StatusCode = 500;
        this._logger.LogError(e, "ExcelImportFail");
        return this.Json(new { success = false,  err = e.GetBaseException().Message });
      }
        }
    //Download the template
    public async Task<IActionResult> Download(string file) {
      
      this.Response.Cookies.Append("fileDownload", "true");
      var path = Path.Combine(this._webHostEnvironment.ContentRootPath, file);
      var downloadFile = new FileInfo(path);
      if (downloadFile.Exists)
      {
       var fileName = downloadFile.Name;
       var mimeType = MimeTypeConvert.FromExtension(downloadFile.Extension);
       var fileContent = new byte[Convert.ToInt32(downloadFile.Length)];
        using (var fs = downloadFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          await fs.ReadAsync(fileContent, 0, Convert.ToInt32(downloadFile.Length));
        }
        return this.File(fileContent, mimeType, fileName);
      }
      else
      {
        throw new FileNotFoundException($"File {file} does not exist!");
      }
    }

    }
}
