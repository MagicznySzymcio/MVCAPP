using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCAPP.Models;

namespace MVCAPP.Data
{
    public interface IUserManager
    {
        // CookieUser Register(RegisterVm model);
        // CookieUser Validate(LoginViewModel model);
        List<User> Get();
        CookieUser Validate(LoginViewModel model);
        Task Login(HttpContext httpContext, CookieUser cookie, bool isPersistent);
        Task LogOut(HttpContext httpContext);
    }

    public class UserManager : IUserManager
    {
        private readonly AppDbContext _db;

        public UserManager(AppDbContext db)
        {
            _db = db;
        }

        public List<User> Get()
        {
            List<User> ListOfUsers = _db.Users.ToList();
            return ListOfUsers;
        }
        public CookieUser Validate(LoginViewModel model)
        {
            var result = _db.Users.Where(x => x.Username == model.Username && x.Password == model.Password)
                .Select(x => new CookieUser
                {
                    Id = x.Id,
                    Username = x.Username
                });

            return result.FirstOrDefault();
        }

        public async Task Login(HttpContext httpContext, CookieUser cookie, bool isPersistent)
        {
            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var claims = SetClaims(cookie);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                // AllowRefresh = <bool>,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = isPersistent,
                IssuedUtc = cookie.CreationDate,
                // RedirectUri = "~/Account/Index"
            };

            await httpContext.SignInAsync(authenticationScheme, claimsPrincipal, authProperties);
        }

        public async Task LogOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private List<Claim> SetClaims(CookieUser cookie)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, cookie.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, cookie.Username));
            return claims;
        }
    }
}
