
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApplication
{

    public class HomeController : Controller
    {
        public IActionResult Index(){
            return View();
        }

        public IActionResult About(){
            return View();
        }

        [Authorize]
        public IActionResult SecureAbout(){
            return View();
        }

        public IActionResult SignOut(){
            var callBackUrl = Url.Action("Index", "Home", values: null, protocol: Request.Scheme);

            return SignOut(new AuthenticationProperties { RedirectUri = callBackUrl},
                CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}