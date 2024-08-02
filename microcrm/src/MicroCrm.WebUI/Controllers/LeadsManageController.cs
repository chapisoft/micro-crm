using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MicroCrm.WebUI.Controllers
{
  public class LeadsManageController : Controller
  {
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

      return View();
    }

    // GET: LeadsManageController/Details/5
    public ActionResult Details(int id)
    {
      return View();
    }
  }
}
