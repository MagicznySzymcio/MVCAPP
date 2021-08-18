using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAPP.Data;
using MVCAPP.Models;

namespace MVCAPP.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public IActionResult Login()
        {
            return View();
            // return Ok(_userManager.Get());
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var cookie = _userManager.Validate(model);
            
            if (cookie == null) return View(model);

            await _userManager.Login(this.HttpContext, cookie, model.IsPersistent);

            return LocalRedirect("~/Home/Index");

            // return Ok(user.Id + " " + user.Username + " " + user.CreationDate);

            // await _userManager.SignIn(this.HttpContext, user, false);

            //return LocalRedirect("~/Home/Index");
        }

        
        public async Task<IActionResult> Logout()
        {
            await _userManager.LogOut(this.HttpContext);
            return RedirectPermanent("~/Home/Index");
        }

    }
}
