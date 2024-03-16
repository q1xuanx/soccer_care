using Microsoft.AspNetCore.Mvc;

namespace Soccer_Care.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManagePitch()
        {
            return View();
        }
        public IActionResult ManageUser()
        {
            return View();
        }
    }
}
