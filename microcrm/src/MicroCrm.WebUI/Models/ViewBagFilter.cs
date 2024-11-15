﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MicroCrm.WebUI.Models
{
  public class ViewBagFilter : IActionFilter
  {
    private readonly SmartSettings _settings;
    public ViewBagFilter(SmartSettings settings)
    {
      _settings = settings;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (context.Controller is Controller controller)
      {
        var claimsidentity = (ClaimsIdentity)controller.User.Identity;
        var username = claimsidentity.FindFirst(ClaimTypes.Name)?.Value;
        var givenname = claimsidentity.FindFirst(ClaimTypes.GivenName)?.Value;
        var email = claimsidentity.FindFirst(ClaimTypes.Email)?.Value;
        var mobilephone = claimsidentity.FindFirst(ClaimTypes.MobilePhone)?.Value;
        var homephone = claimsidentity.FindFirst(ClaimTypes.HomePhone)?.Value;
        var otherphone = claimsidentity.FindFirst(ClaimTypes.OtherPhone)?.Value;
        var avatarurl = claimsidentity.FindFirst("http://schemas.microsoft.com/identity/claims/avatarurl")?.Value;
        var tenantid = claimsidentity.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value;
        var tenantname = claimsidentity.FindFirst("http://schemas.microsoft.com/identity/claims/tenantname")?.Value;
        var tenantdb = claimsidentity.FindFirst("http://schemas.microsoft.com/identity/claims/tenantdb")?.Value;
        var culture = claimsidentity.FindFirst(ClaimTypes.Locality)?.Value;
        var country = claimsidentity.FindFirst(ClaimTypes.Country)?.Value;
        var role = claimsidentity.FindFirst(ClaimTypes.Role)?.Value;
        // MicroCrm Toggle Features
        controller.ViewBag.AppSidebar = _settings.Features.AppSidebar;
        controller.ViewBag.AppHeader = _settings.Features.AppHeader;
        controller.ViewBag.AppLayoutShortcut = _settings.Features.AppLayoutShortcut;
        controller.ViewBag.AppFooter = _settings.Features.AppFooter;
        controller.ViewBag.ShortcutMenu = _settings.Features.ShortcutMenu;
        controller.ViewBag.ChatInterface = _settings.Features.ChatInterface;
        controller.ViewBag.LayoutSettings = _settings.Features.LayoutSettings;

        // MicroCrm Default Settings
        controller.ViewBag.App = _settings.App;
        controller.ViewBag.AppName = _settings.AppName;
        controller.ViewBag.AppFlavor = _settings.AppFlavor;
        controller.ViewBag.AppFlavorSubscript = _settings.AppFlavorSubscript;
        controller.ViewBag.User = username;
        controller.ViewBag.Role = role;
        controller.ViewBag.Culture = culture;
        controller.ViewBag.Country = country;
        controller.ViewBag.GivenName = givenname;
        controller.ViewBag.MobilePhone = mobilephone;
        controller.ViewBag.TenantId = tenantid;
        controller.ViewBag.Email = email;
        controller.ViewBag.Twitter = givenname;
        controller.ViewBag.Avatar = avatarurl;
        controller.ViewBag.AvatarM = avatarurl;
        controller.ViewBag.Version = _settings.Version;
        controller.ViewBag.ThemeVersion = _settings.Theme.ThemeVersion;
        controller.ViewBag.Logo = _settings.Logo;
        controller.ViewBag.LogoM = _settings.LogoM;
        controller.ViewBag.Copyright = $"2020 © { _settings.AppName} &nbsp; { _settings.Author} <a href='https://crm.microtec.vn/' class='text-primary fw-500' title='ICP {_settings.ICP}' target='_blank'>ICP {_settings.ICP} </a>";
        controller.ViewBag.CopyrightInverse = $"2020 © { _settings.AppName} &nbsp;{ _settings.Author} <a href='https://crm.microtec.vn/' class='text-primary fw-500' title='ICP {_settings.ICP}' target='_blank'>ICP {_settings.ICP} </a>";
      }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
  }
}
