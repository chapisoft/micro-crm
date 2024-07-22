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
      NLog.LogManager.GetCurrentClassLogger().Trace("Visit Homepage");
      this.logger.LogTrace("Visit Homepage");
      this.logger.LogDebug("Visit Homepage");
      this.logger.LogInformation("Visit Homepage");
      _eventBus.Publish("MicroCrm.eventbus", new SubscribeEventData()
      {
        publisher = typeof(HomeController).Name,
        content = $"欢迎来到Homepage @ {DateTime.Now}",
        from = ViewBag.GivenName,
        group = "Operate日志",
        title = "Visit Homepage",
        url = "/Home/Index"
      });
      return View();
      }


  }
}
