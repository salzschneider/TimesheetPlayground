using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetPlayground.UI.BLL;
using TimesheetPlayground.UI.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace TimesheetPlayground.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IUserService userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthUserViewModel authUser)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.GetUserByNamePasswordAsync(authUser.Username, authUser.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Login failed. Please check Username and/or password");
                    return View();
                }
                else
                {
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Id)),
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Role,user.Role.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Timesheet");
                }
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
