﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MicroCrm.WebUI.Data.Models;

namespace MicroCrm.WebUI.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  [IgnoreAntiforgeryToken]
  public class LoginModel : PageModel
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public class InputModel
    {
      [Required]
      public string UserName { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string returnUrl = null)
    {
      if (!string.IsNullOrEmpty(ErrorMessage))
      {
        ModelState.AddModelError(string.Empty, ErrorMessage);
      }

      returnUrl = returnUrl ?? Url.Content("~/");

      // Clear the existing external cookie to ensure a clean login process
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
      await _signInManager.SignOutAsync();
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

      ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl = returnUrl ?? Url.Content("~/");

      if (ModelState.IsValid)
      {
        var valid = new EmailAddressAttribute().IsValid(Input.UserName);
        ApplicationUser loginUser = null;
        if (valid)
        {
          loginUser = await _userManager.FindByEmailAsync(Input.UserName);
        }
        else {
          loginUser = await _userManager.FindByNameAsync(Input.UserName);
        }
        if (loginUser == null) {
          ModelState.AddModelError(string.Empty, "Identity或Email不存在.");
          return Page();
        }
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(loginUser, Input.Password, Input.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
          _logger.LogInformation($"{loginUser.UserName}:Login成功");
          return LocalRedirect(returnUrl);
        }
        if (result.RequiresTwoFactor)
        {
          return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        }
        if (result.IsLockedOut)
        {
          _logger.LogInformation($"{loginUser.UserName}:Account is locked");
          ModelState.AddModelError(string.Empty, "Account is locked,Try again after 15 minutes.");
          return Page();
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Username或Password不正确" );
          return Page();
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }
  }
}
