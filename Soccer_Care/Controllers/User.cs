using Microsoft.AspNetCore.Mvc;

namespace Soccer_Care.Controllers
{
    public class User : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
