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
            var listUser = _context.User.ToList();
            ViewBag.ListType = _context.Role.ToList();
            return View(listUser);
        }
        [HttpPost]
        public IActionResult SearchUser(String IDRole)
        {
            ViewBag.IDRole = IDRole;
            ViewBag.ListType = _context.Role.ToList();
            List<UserModel> listUser = new List<UserModel>();
            if (IDRole != "0")
            {
                listUser = _context.User.Where(i => i.IDRole == IDRole).ToList();
            }else
            {
                listUser = _context.User.ToList();
            }
            return View("ManageUser", listUser);
        }
    }
}
