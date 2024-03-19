using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;

namespace Soccer_Care.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly SoccerCareDbContext _context;

        public AdminController(SoccerCareDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManagePitch()
        {
            var listPitch = _context.FootBallFields.ToList();
            return View(listPitch);
        }
        public IActionResult ManageUser()
        {
            return View();
        }
    }
}
