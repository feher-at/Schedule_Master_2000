﻿using System;
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

namespace Schedule_master_2000.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // create and add user
                // then redirect to somewhere
            }

            return View("Registration");
        }

        public IActionResult RegistrationComplete(UserViewModel model)
        {
            if (!Utility.IsValidEmail(model.Email))
            {
                return RedirectToAction("Registration", "Account");
            }

            //_loader.InsertUser(model.Username, model.Email, Utility.Hash(model.Password));

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        //[HttpPost]

        //public async Task<ActionResult> LoginAsync([FromForm]string email, [FromForm] string password)
        //{

        //    if (_loader.CheckIfUserExists(email, password))
        //    {
        //        //here comes the Loginformation storing sql querry;
        //        var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };

        //        var claimsIdentity = new ClaimsIdentity(
        //            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //        var authProperties = new AuthenticationProperties
        //        {
        //            //AllowRefresh = <bool>,
        //            // Refreshing the authentication session should be allowed.

        //            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
        //            // The time at which the authentication ticket expires. A 
        //            // value set here overrides the ExpireTimeSpan option of 
        //            // CookieAuthenticationOptions set with AddCookie.

        //            //IsPersistent = true,
        //            // Whether the authentication session is persisted across 
        //            // multiple requests. When used with cookies, controls
        //            // whether the cookie's lifetime is absolute (matching the
        //            // lifetime of the authentication ticket) or session-based.

        //            //IssuedUtc = <DateTimeOffset>,
        //            // The time at which the authentication ticket was issued.

        //            //RedirectUri = <string>
        //            // The full path or absolute URI to be used as an http 
        //            // redirect response value.
        //        };

        //        await HttpContext.SignInAsync(
        //            CookieAuthenticationDefaults.AuthenticationScheme,
        //            new ClaimsPrincipal(claimsIdentity),
        //            authProperties);

        //        return RedirectToAction("ProfileDetails", "Profile");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Account");


        //    }
        //}
    }
}