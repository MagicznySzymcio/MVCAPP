using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAPP.Models;

namespace MVCAPP.Data
{
    public interface IUserManager
    {
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
            var userList = _db.Users.Include(x => x.Role).Where(x => x.Username == model.Username).AsEnumerable();

            var result = userList.Where(x => x.Password == Hasher.GenerateHash(model.Password, x.Salt))
                .Select(x => new CookieUser
                {
                    Id = x.Id,
                    Username = x.Username,
                    Role = x.Role.Type
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
            claims.Add(new Claim(ClaimTypes.Role, cookie.Role));
            return claims;
        }
    }
}
