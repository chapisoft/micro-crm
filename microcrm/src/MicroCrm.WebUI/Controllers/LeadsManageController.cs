using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroCrm.WebUI.Controllers
{
  public class LeadsManageController : Controller
  {
    // GET: LeadsManageController
    public ActionResult Index()
    {
      return View();
    }

    // GET: LeadsManageController/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }
  }
}
