using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MicroCrm.Dto;
using MicroCrm.Service;
using MicroCrm.WebUI.Data.Models;
using MicroCrm.WebUI.Models;

namespace MicroCrm.WebUI.ViewComponents
{
  public class NavigationViewComponent : ViewComponent
  {
    private readonly IRoleMenuService _menuService;
    private readonly UserManager<ApplicationUser> _userManager;
    public NavigationViewComponent(
      UserManager<ApplicationUser> userManager,
      IRoleMenuService menuService)
    {
      _menuService = menuService;
      _userManager = userManager;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
      var userName = this.User.Identity.Name;
      var user = await _userManager.FindByNameAsync(userName);
      var roles = await this._userManager.GetRolesAsync(user);
      var items = await _menuService.NavDataSource(roles.ToArray()); //NavigationModel.Full;
      return View(new SmartNavigation(items));
    }
  }
}
