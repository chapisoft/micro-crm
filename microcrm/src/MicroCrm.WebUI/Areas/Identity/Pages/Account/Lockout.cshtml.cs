﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MicroCrm.WebUI.Data.Models;

namespace MicroCrm.WebUI.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  [IgnoreAntiforgeryToken]
  public class LockoutModel : PageModel
  {
    private readonly ILogger<LogoutModel> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty]
    public InputModel Input { get; set; }
    public class InputModel
    {
      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }
      [Required]
      public  string UserName { get; set; }
    }

    public LockoutModel(
      UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ILogger<LogoutModel> logger)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
    }

    public async Task OnGetAsync()
    {
      if (this.User.Identity.IsAuthenticated)
      {
        var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
        this.PageContext.HttpContext.Response.Cookies.Append("UserName", user.UserName);
        this.PageContext.HttpContext.Response.Cookies.Append("GivenName", user.GivenName);
        this.PageContext.HttpContext.Response.Cookies.Append("Avatars", user.AvatarUrl);
      }
      
      await _signInManager.SignOutAsync();
       
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl = returnUrl ?? Url.Content("~/");

      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var userName = Input.UserName;
        var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, true, lockoutOnFailure: true);

        if (result.Succeeded)
        {
          _logger.LogInformation($"{userName}:Unlock successfully");
          return LocalRedirect(returnUrl);
        }
        if (result.RequiresTwoFactor)
        {
          return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = true });
        }
        if (result.IsLockedOut)
        {
          _logger.LogInformation($"{userName}:Account is locked");
          ModelState.AddModelError(string.Empty, "Account is locked,Try again after 15 minutes.");
          return Page();
        }
        else
        {
          ModelState.AddModelError(string.Empty, "The password is incorrect.");
          return Page();
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }
  }
}
