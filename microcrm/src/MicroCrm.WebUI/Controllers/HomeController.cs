using System;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroCrm.Dto;
using URF.Core.Abstractions;
using MicroCrm.Service.Implementation;

namespace MicroCrm.WebUI.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private readonly ICapPublisher _eventBus;
    private readonly IDashboardService _dashboardService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> logger;

    public HomeController(
      ICapPublisher eventBus,
        IDashboardService dashboardService,
        IUnitOfWork unitOfWork,
        ILogger<HomeController> logger)
    {
      _eventBus = eventBus;
      _dashboardService = dashboardService;
      _unitOfWork = unitOfWork;
      this.logger = logger;
   
    }

    public IActionResult Index() {
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
      string username = ViewBag.GivenName;
      string role = ViewBag.Role;
      var result = _dashboardService.Get(request, username, role);
      return View();
      }


  }
}
