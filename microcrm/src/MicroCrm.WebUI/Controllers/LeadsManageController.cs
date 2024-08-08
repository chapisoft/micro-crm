using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicroCrm.WebUI.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MicroCrm.Domain.Models;
using MicroCrm.Service;
using Newtonsoft.Json;

namespace MicroCrm.WebUI.Controllers
{
  public class LeadsManageController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IContactService _contactService;
    private readonly IProjectService _pojectService;
    private readonly IContactActivityService _activityService;
    private readonly IQuotationService _quotationService;
    public LeadsManageController(UserManager<ApplicationUser> userManager,
          IContactService contactService, IProjectService pojectService,
          IContactActivityService activityService, IQuotationService quotationService)
    {
      _userManager = userManager;
      _contactService = contactService;
      _pojectService = pojectService;
      _activityService = activityService;
      _quotationService = quotationService;
    }
    // GET: LeadsManageController
    public ActionResult Index()
    {
      var role = (string)ViewBag.Role;

      var selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Mobile", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "Chat", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "Mail", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "Meet", Value = "3" });
      selectlist.Add(new SelectListItem() { Text = "Other", Value = "4" });
      ViewBag.Channel = selectlist;

      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Pending", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "On - going", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "LOST", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "SUBMIT", Value = "3" });
      if (role.ToLower() == "admin")
        selectlist.Add(new SelectListItem() { Text = "SOLD", Value = "4" });
      ViewBag.ProjectStatus = selectlist;

      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Public", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "Private", Value = "1" });
      ViewBag.Private = selectlist;

      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Email", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "Exhibition", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "Incoming phone", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "Website", Value = "3" });
      selectlist.Add(new SelectListItem() { Text = "Transfer", Value = "4" });
      ViewBag.Source = selectlist;

      selectlist = new List<SelectListItem>();
      string filterRules = "";
      var filters = PredicateBuilder.FromFilter<ApplicationUser>(filterRules);
      var users = this._userManager.Users.Where(filters).OrderBy($"{"Id"}  {"desc"}");
      var datalist = users.Select(n => new { GivenName = n.GivenName, UserName = n.UserName }).ToList();
      foreach (var item in datalist)
      {
        if (!item.UserName.ToLower().Equals("sa"))
          selectlist.Add(new SelectListItem() { Text = item.GivenName, Value = item.UserName });
      }
      ViewBag.Users = selectlist;

      return View();
    }

    // GET: LeadsManageController/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }

    public async Task<JsonResult> SetCompany(int companyId = 0)
    {
      try
      {
        if (companyId > 0)
          HttpContext.Session.SetInt32("CompanyId", companyId);

        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }
    public async Task<JsonResult> SetContact(int contactId = 0)
    {
      try
      {
        if (contactId > 0)
          HttpContext.Session.SetInt32("ContactId", contactId);
        return Json(new { success = true });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }
    public async Task<IActionResult> AddOrEditProject(int id = 0)
    {
      Project model = new Project();
      model.CompanyId = (int)HttpContext.Session.GetInt32("CompanyId");
      var datalist = _contactService.Queryable().Where(e => e.CompanyId.Equals(model.CompanyId))
                         .OrderBy(n => n.Name)
                         .Select(n => new { Id = n.Id, Name = n.Name, Title = n.Title, Department = n.Department }).ToList();
      var selectlist = new List<SelectListItem>();
      foreach (var item in datalist)
      {
        selectlist.Add(new SelectListItem() { Text = item.Name + " (" + item.Title + "-" + item.Department + ")", Value = item.Id.ToString() });
      }
      ViewBag.Contacts = selectlist;

      var role = (string)ViewBag.Role;
      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Pending", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "On - going", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "LOST", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "SUBMIT", Value = "3" });
      if (role.ToLower() == "admin")
        selectlist.Add(new SelectListItem() { Text = "SOLD", Value = "4" });
      ViewBag.ProjectStatus = selectlist;

      selectlist = new List<SelectListItem>();
      string filterRules = "";
      var filters = PredicateBuilder.FromFilter<ApplicationUser>(filterRules);
      var users = _userManager.Users.Where(filters).OrderBy($"{"Id"}  {"desc"}");
      var list = users.Select(n => new { GivenName = n.GivenName, UserName = n.UserName }).ToList();
      foreach (var item in list)
      {
        if (!item.UserName.ToLower().Equals("sa"))
          selectlist.Add(new SelectListItem() { Text = item.GivenName, Value = item.UserName });
      }
      ViewBag.Users = selectlist;

      if (id > 0)
        model = _pojectService.Queryable().FirstOrDefault(e => e.Id.Equals(id));
      return PartialView(model);
    }
    public async Task<IActionResult> AddOrEditActivity(int id = 0)
    {
      ContactActivity model = new ContactActivity();
      model.CompanyId = (int)HttpContext.Session.GetInt32("CompanyId");
      var datalist = _contactService.Queryable().Where(e => e.CompanyId.Equals(model.CompanyId))
                         .OrderBy(n => n.Name)
                         .Select(n => new { Id = n.Id, Name = n.Name, Title = n.Title, Department = n.Department }).ToList();
      var selectlist = new List<SelectListItem>();
      foreach (var item in datalist)
      {
        selectlist.Add(new SelectListItem() { Text = item.Name + " (" + item.Title + "-" + item.Department + ")", Value = item.Id.ToString() });
      }
      ViewBag.Contacts = selectlist;

      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Mobile", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "Chat", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "Mail", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "Meet", Value = "3" });
      selectlist.Add(new SelectListItem() { Text = "Other", Value = "4" });
      ViewBag.Channel = selectlist;

      if (id > 0)
        model = _activityService.Queryable().FirstOrDefault(e => e.Id.Equals(id));
      return PartialView(model);
    }
    public async Task<IActionResult> AddOrEditQuotation(int id = 0)
    {
      Quotation model = new Quotation();
      model.CompanyId = (int)HttpContext.Session.GetInt32("CompanyId");
      var datalist = _contactService.Queryable().Where(e => e.CompanyId.Equals(model.CompanyId))
                         .OrderBy(n => n.Name)
                         .Select(n => new { Id = n.Id, Name = n.Name, Title = n.Title, Department = n.Department }).ToList();
      var selectlist = new List<SelectListItem>();
      foreach (var item in datalist)
      {
        selectlist.Add(new SelectListItem() { Text = item.Name + " (" + item.Title + "-" + item.Department + ")", Value = item.Id.ToString() });
      }
      ViewBag.Contacts = selectlist;

      var role = (string)ViewBag.Role;
      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "Pending", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "On - going", Value = "1" });
      selectlist.Add(new SelectListItem() { Text = "LOST", Value = "2" });
      selectlist.Add(new SelectListItem() { Text = "SUBMIT", Value = "3" });
      if (role.ToLower() == "admin")
        selectlist.Add(new SelectListItem() { Text = "SOLD", Value = "4" });
      ViewBag.ProjectStatus = selectlist;

      if (id > 0)
        model = _quotationService.Queryable().FirstOrDefault(e => e.Id.Equals(id));
      return PartialView(model);
    }
  }
}
