using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule_master_2000.Domain;
using Schedule_master_2000.ViewModels;
using Schedule_master_2000.Services;
using Microsoft.AspNetCore.Identity;
using Schedule_master_2000.Models;

namespace Schedule_master_2000.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserActivityService _userActivityService;

        public AccountController(IUserService userService, IUserActivityService userActivityService)
        {
            _userService = userService;
            _userActivityService = userActivityService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult LoginRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (!Utility.IsValidEmail(model.Email))
            {
                return RedirectToAction("Registration", "Account");
            }

            _userService.InsertUser(model.Username, model.Password, model.Email);

            User user = _userService.GetOne(model.Email);
            _userActivityService.InsertActivity(user.ID, "User registered on the site ", DateTime.Now);

           

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<ActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (_userService.ValidateUser(model.Email, model.Password))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Email, model.Email) };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                User user = _userService.GetOne(model.Email);
                _userActivityService.InsertActivity(user.ID, "User login on the site ", DateTime.Now);
               

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect E-mail and/or Password. Please try again.");
                return View("Login", model);
            }
        }

        [HttpGet]
        public IActionResult UserAccount()
        {
            string email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

            User user = _userService.GetOne(email);

            return View(user);
        }

        [HttpGet]
        public IActionResult ModifyUser()
        {
            string email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

            User user = _userService.GetOne(email);
            ModifyViewModel modifyModel = new ModifyViewModel()
            {
                User = user,
                Modification = null
            };

            return View(modifyModel);
        }

        [HttpPost]
        public IActionResult ModifyUser([FromForm(Name = "Modification.Id")] int id,
            [FromForm(Name = "Modification.Username")] string username, [FromForm(Name = "Modification.Email")] string email,
            [FromForm(Name = "Modification.Password")] string password)
        {
            _userService.UpdateUser(id, username, password, email);

            _userActivityService.InsertActivity(id, "User modificated he/she's profile", DateTime.Now);
          
            return RedirectToAction("LogOutAndPromoteLogin", "Account");
        }

        [HttpGet]
        public async Task<ActionResult> LogOutAndPromoteLoginAsync()
        {
            var user = HttpContext.User;
            var claim = user.Claims.First(c => c.Type == ClaimTypes.Email);
            var email = claim.Value;
            User currentUser = _userService.GetOne(email);
            _userActivityService.InsertActivity(currentUser.ID, "User logout", DateTime.Now);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Login", "Account");
        }

    }
}