using Microsoft.AspNetCore.Mvc;

namespace Soccer_Care.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
