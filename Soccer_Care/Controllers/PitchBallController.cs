using Microsoft.AspNetCore.Mvc;

namespace Soccer_Care.Controllers
{
    public class PitchBallController : Controller
    {
        IHttpContextAccessor contextAccessor;
        public PitchBallController(IHttpContextAccessor contextAccessor) {
            this.contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
