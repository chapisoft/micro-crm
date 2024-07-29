using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroCrm.Infrastructure.Persistence
{
  public static class MicroCrmDbContextSeed
  {
    

    public static async Task SeedSampleDataAsync(MicroCrmDbContext context)
    {
      //Seed, if necessary
      if (!context.MenuItems.Any())
      {

        var home = new Domain.Models.MenuItem()
        {
          Title = "Home",
          Description = "Home",
          Action = "Index",
          Controller = "Home",
          Url = "/Home/Index",
          LineNum = "001",
          Icon = "fal fa-home",
          IsEnabled = true,
          
        };
        var menu1 = new Domain.Models.MenuItem()
        {
          Title = "CRM",
          Description = "CRM",
          Action = "#",
          Controller = "#",
          Url = "#",
          LineNum = "002",
          Icon = "fal fa-folders",
          IsEnabled = true,
          Children = new HashSet<Domain.Models.MenuItem>()
          {
            new Domain.Models.MenuItem(){
              Title = "Leads Manage",
              Description = "Leads Manage",
              Action = "Index",
              Controller = "Leads",
              Url = "/Leads/Index",
              LineNum = "201",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Companies",
              Description = "Companies",
              Action = "Index",
              Controller = "Companies",
              Url = "/Companies/Index",
              LineNum = "202",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Customers",
              Description = "Customers",
              Action = "Index",
              Controller = "Customers",
              Url = "/Customers/Index",
              LineNum = "203",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Products",
              Description = "Products",
              Action = "Index",
              Controller = "Products",
              Url = "/Products/Index",
              LineNum = "204",
              Icon = "",
              IsEnabled = true,
            },
             new Domain.Models.MenuItem(){
              Title = "Orders",
              Description = "Orders",
              Action = "Index",
              Controller = "Orders",
              Url = "/Orders/Index",
              LineNum = "205",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Photos",
              Description = "Photos",
              Action = "index",
              Controller = "Photos",
              Url = "/photos/index",
              LineNum = "206",
              Icon = "",
              IsEnabled = true,
            },
          }
        };
        var menu2 = new Domain.Models.MenuItem()
        {
          Title = "System Management",
          Description = "System Management",
          Action = "#",
          Controller = "#",
          Url = "#",
          LineNum = "009",
          Icon = "fal fa-users-cog",
          IsEnabled = true,
          Children = new HashSet<Domain.Models.MenuItem>()
          {
            new Domain.Models.MenuItem(){
              Title = "Tenants",
              Description = "Tenants",
              Action = "Index",
              Controller = "Tenants",
              Url = "/Tenants/Index",
              LineNum = "901",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Account Management",
              Description = "Account Management",
              Action = "Index",
              Controller = "AccountManage",
              Url = "/AccountManage/Index",
              LineNum = "902",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Role Manage",
              Description = "Role Manage",
              Action = "Index",
              Controller = "RoleManage",
              Url = "/RoleManage/Index",
              LineNum = "903",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Role Menus",
              Description = "Role Menus",
              Action = "Index",
              Controller = "rolemenus",
              Url = "/rolemenus/index",
              LineNum = "904",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Menu",
              Description = "Menu",
              Action = "Index",
              Controller = "MenuItems",
              Url = "/MenuItems/Index",
              LineNum = "905",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Key-value pair Config",
              Description = "Key-value pair Config",
              Action = "Index",
              Controller = "CodeItems",
              Url = "/CodeItems/Index",
              LineNum = "906",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Import & Export Config",
              Description = "Import & Export Config",
              Action = "Index",
              Controller = "DataTableImportMappings",
              Url = "/DataTableImportMappings/Index",
              LineNum = "907",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "System log",
              Description = "System log",
              Action = "Index",
              Controller = "Logs",
              Url = "/Logs/Index",
              LineNum = "908",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "Notifications",
              Description = "Notifications",
              Action = "Index",
              Controller = "notifications",
              Url = "/notifications/index",
              LineNum = "908",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "WebApi",
              Description = "WebApi",
              Action = "Index",
              Controller = "swagger",
              Url = "/swagger/index.html",
              LineNum = "909",
              Icon = "",
              IsEnabled = true,
            },
            new Domain.Models.MenuItem(){
              Title = "CAP EventBus",
              Description = "CAP EventBus",
              Action = "",
              Controller = "cap",
              Url = "/cap",
              LineNum = "910",
              Icon = "",
              IsEnabled = true,
            },
            
          }
        };
        context.MenuItems.Add(home);
        context.MenuItems.Add(menu2);
        context.MenuItems.Add(menu1);
        await context.SaveChangesAsync();
        var items=context.MenuItems.Include(x=>x.Parent).ToList();
        var roles = new string[] { "Admin", "Basic" };
        foreach(var role in roles)
        {
          foreach(var item in items)
          {
            var rolemenu = new Domain.Models.RoleMenu()
            {
               RoleName = role,
               MenuId=item.Id,
               IsEnabled = true,
            };
            if (role == "Basic" && (item.Title == "System Management" || item?.Parent?.Title== "System Management")){
              continue;
            }
            context.RoleMenus.Add(rolemenu);
          }
        }
        await context.SaveChangesAsync();
      }
      if (!context.CodeItems.Any())
      {
        context.CodeItems.Add(new  Domain.Models.CodeItem() {  CodeType = "Status",  Code = "initialization", Text = "initialization", Description = "Status of workflow" });
        context.CodeItems.Add(new Domain.Models.CodeItem() { CodeType = "Status", Code = "processing", Text = "processing", Description = "Status of workflow" });
        context.CodeItems.Add(new Domain.Models.CodeItem() { CodeType = "Status", Code = "pending", Text = "pending", Description = "Status of workflow" });
        context.CodeItems.Add(new Domain.Models.CodeItem() { CodeType = "Status", Code = "finished", Text = "finished", Description = "Status of workflow" });
       
        await context.SaveChangesAsync();
      }
       
    }
  }
}
