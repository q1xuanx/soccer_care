using Microsoft.AspNetCore.Mvc;

namespace Soccer_Care.Controllers
{
    public class PitchBallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
