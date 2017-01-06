
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApplication
{

    public class HomeController : Controller
    {
        public IActionResult Index(){
            return View();
        }
    }
}