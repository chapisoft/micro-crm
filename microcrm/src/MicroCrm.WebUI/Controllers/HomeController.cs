using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MicroCrm.WebUI.Data.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MicroCrm.Dto;
using URF.Core.Abstractions;
using MicroCrm.WebUI.Data;
using StoredProcedureEFCore;
using DotNetCore.CAP;
//using MicroCrm.Service.Implementation;

namespace MicroCrm.WebUI.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICapPublisher _eventBus;
    //private readonly IDashboardService _dashboardService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<HomeController> logger;

    public HomeController(UserManager<ApplicationUser> userManager,
      ICapPublisher eventBus,
        //IDashboardService dashboardService,
        IUnitOfWork unitOfWork,
      ApplicationDbContext dbContext,
        ILogger<HomeController> logger)
    {
      _userManager = userManager;
      _eventBus = eventBus;
      //_dashboardService = dashboardService;
      _unitOfWork = unitOfWork;
      this.logger = logger;
      _dbContext = dbContext;

    }

    public IActionResult Index()
    {
      NLog.LogManager.GetCurrentClassLogger().Trace("Visit Homepage");
      this.logger.LogTrace("Visit Homepage");
      this.logger.LogDebug("Visit Homepage");
      this.logger.LogInformation("Visit Homepage");
      _eventBus.Publish("MicroCrm.eventbus", new SubscribeEventData()
      {
        publisher = typeof(HomeController).Name,
        content = $"Welcome to MicroCRM @ {DateTime.Now}",
        from = ViewBag.GivenName,
        group = "Operate log",
        title = "Visit Homepage",
        url = "/Home/Index"
      });
      BaseRequest request = new BaseRequest();
      request.TenantId = 1;

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

      selectlist = new List<SelectListItem>();
      selectlist.Add(new SelectListItem() { Text = "This Week", Value = "0" });
      selectlist.Add(new SelectListItem() { Text = "Last Week", Value = "1" });
      ViewBag.Times = selectlist;

      return View();
    }
    public JsonResult GetData()
    {
      try
      {
        string username = ViewBag.User;
        string role = ViewBag.Role;
        int times = 0;
        Dashboard result = new Dashboard();
        List<StatisticYear> year = null;
        _dbContext.LoadStoredProc("dbo.Proc_Dashboard_Get_ActivityYear")
        .AddParam("User", username)
        .AddParam("Role", role)
        .Exec(r => year = r.ToList<StatisticYear>());
        result.StatisticYear = year;

        List<StatisticStatus> status = null;
        _dbContext.LoadStoredProc("dbo.Proc_Dashboard_Get_ProjectStatus")
        .AddParam("User", username)
        .AddParam("Role", role)
        .Exec(r => status = r.ToList<StatisticStatus>());
        result.StatisticStatus = status;

        List<StatisticRank> rank = null;
        _dbContext.LoadStoredProc("dbo.Proc_Dashboard_Get_ActivityRank")
        .AddParam("User", username)
        .AddParam("Role", role)
        .Exec(r => rank = r.ToList<StatisticRank>());
        result.StatisticRank = rank;

        List<StatisticActivity> activity = null;
        if (role.Equals("Saler"))
        {
          _dbContext.LoadStoredProc("dbo.Proc_Dashboard_Get_ActivityBySaler")
          .AddParam("User", username)
          .AddParam("Role", role)
        .AddParam("Times", times)
          .Exec(r => activity = r.ToList<StatisticActivity>());
        }
        else
        {
          _dbContext.LoadStoredProc("dbo.Proc_Dashboard_Get_ActivityBySaler")
          .AddParam("Role", role)
        .AddParam("Times", times)
          .Exec(r => activity = r.ToList<StatisticActivity>());
        }
        result.StatisticActivity = activity;
        return Json(new { success = true, result });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }
    public JsonResult GetStatisticActivity(string username, int times)
    {
      try
      {
        string role = ViewBag.Role;

        List<StatisticActivity> activity = null;
        if (role.Equals("Saler"))
          username = ViewBag.User;
        _dbContext.LoadStoredProc("dbo.Proc_Dashboard_Get_ActivityBySaler")
        .AddParam("User", username)
        .AddParam("Role", role)
        .AddParam("Times", times)
        .Exec(r => activity = r.ToList<StatisticActivity>());

        return Json(new { success = true, activity });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }

  }
}
