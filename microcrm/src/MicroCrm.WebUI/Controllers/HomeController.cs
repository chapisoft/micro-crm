using System;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroCrm.Dto;
using URF.Core.Abstractions;
using MicroCrm.WebUI.Data;
using MicroCrm.WebUI.Data.Models;
using StoredProcedureEFCore;
using System.Collections.Generic;
using System.Threading.Tasks;
//using MicroCrm.Service.Implementation;

namespace MicroCrm.WebUI.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private readonly ICapPublisher _eventBus;
    //private readonly IDashboardService _dashboardService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<HomeController> logger;

    public HomeController(
      ICapPublisher eventBus,
        //IDashboardService dashboardService,
        IUnitOfWork unitOfWork,
      ApplicationDbContext dbContext,
        ILogger<HomeController> logger)
    {
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

      return View();
    }
    public JsonResult GetData()
    {
      try
      {
        string username = ViewBag.GivenName;
        string role = ViewBag.Role;
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
        return Json(new { success = true, result });
      }
      catch (Exception e)
      {
        return Json(new { success = false, err = e.Message });
      }
    }

  }
}
