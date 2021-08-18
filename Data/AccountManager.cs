using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVCAPP.Data
{
    public interface AccountManager
    {
        Task SignIn(HttpContext httpContext, CookieUser user, bool isPersistent = false);
        Task SignOut(HttpContext httpContext);
    }
}
