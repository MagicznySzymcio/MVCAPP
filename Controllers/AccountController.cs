using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAPP.Data;
using MVCAPP.Models;

namespace MVCAPP.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly IUserManager _userManager;
        private readonly AppDbContext _context;

        public AccountController(IUserManager userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login(string message = "git gut")
        {
            if (User.Identity.IsAuthenticated)
                return LocalRedirect("~/Home/Index");
            ViewData["Message"] = message;
            return View();
            // return Ok(_userManager.Get());
        }

        public async Task<IActionResult> Index()
        {
            var userInfo = _context.Users.Include(x => x.Role).Where(x => x.Username == HttpContext.User.Identity.Name);
            var user = await userInfo.FirstOrDefaultAsync();
            var Vm = new CredentialsViewModel
            {
                Username = user.Username,
                Role = user.Role
            };
            return View(Vm);
        }

        public IActionResult Register()
        {
            var salt = Hasher.GenerateSalt();
            var hashedPassword = Hasher.GenerateHash("jacek", salt);
            var user = new User
            {
                Username = "jacek",
                Password = hashedPassword,
                Salt = salt,
                RoleId = 1

            };
            _context.Add(user);
            _context.SaveChanges();
            return Ok("zarejestrowano");
        }

        [AllowAnonymous]
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

        
        public async Task<IActionResult> Logout(string message = "Successfully logged out")
        {
            await _userManager.LogOut(this.HttpContext);
            return RedirectPermanent($"~/Account/Login/{message}");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> ChangeCredentials(CredentialsViewModel model)
        {
            // if (!ModelState.IsValid)
            //    return View(model);

            var result = _context.Users.Where(u => u.Username == HttpContext.User.Identity.Name).SingleOrDefaultAsync();
            if (result != null)
            {
                var salt = Hasher.GenerateSalt();
                var hashedPassword = Hasher.GenerateHash(model.Password, salt);
                result.Result.Username = model.Username;
                result.Result.Password = hashedPassword;
                result.Result.Salt = salt;
                _context.SaveChanges();
                return RedirectPermanent($"~/Account/Logout/Succesfully changed, please log in again");
            }


            return Ok("asdasd");
        }

        public IActionResult Test1()
        {
            var xy = _context.Tests.Where(x => x.costam == "jakis_string").FirstOrDefault();

           
            return Ok(xy);
        }

        public IActionResult Test2()
        {
            return View();
        }
    }
}
