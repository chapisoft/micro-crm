using System;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using MicroCrm.Domain.Models;
using MicroCrm.Dto;
using MicroCrm.Service;
using URF.Core.Abstractions;

namespace MicroCrm.WebUI.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    private readonly ICapPublisher _eventBus;
    private readonly ICompanyService  _companyService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> logger;

    public HomeController(
      ICapPublisher eventBus,
        ICompanyService companyService,
        IUnitOfWork unitOfWork,
        ILogger<HomeController> logger)
    {
      _eventBus = eventBus;
      _companyService = companyService;
      _unitOfWork = unitOfWork;
      this.logger = logger;
   
    }

    public IActionResult Index() {
      NLog.LogManager.GetCurrentClassLogger().Trace("访问首页");
      this.logger.LogTrace("访问首页");
      this.logger.LogDebug("访问首页");
      this.logger.LogInformation("访问首页");
      _eventBus.Publish("MicroCrm.eventbus", new SubscribeEventData()
      {
        publisher = typeof(HomeController).Name,
        content = $"欢迎来到首页 @ {DateTime.Now}",
        from = ViewBag.GivenName,
        group = "操作日志",
        title = "访问首页",
        url = "/Home/Index"
      });
      return View();
      }


  }
}
