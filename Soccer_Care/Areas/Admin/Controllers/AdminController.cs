using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;

namespace Soccer_Care.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Partner")]
    public class AdminController : Controller
    {
        private readonly SoccerCareDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(SoccerCareDbContext context, UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var find = _context.DetailsOrder.ToList();
            foreach(DetailsOrderModel details in find)
            {
                details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
            }
            return View(find);
        }
        public IActionResult ManagePitch()
        {
            var listPitch = _context.FootBallFields.ToList();
            return View(listPitch);
        }

        public IActionResult ManageUser()
        {
            var listUser = _context.Users.ToList();
            var listRole = _roleManager.Roles.ToList();
            ViewBag.ListType = listRole;
            return View(listUser);
        }
        [HttpPost]
        public IActionResult SearchUser(String IDRole)
        {
            var listUser = _userManager.GetUsersInRoleAsync(IDRole).Result.ToList();
            var listRole = _roleManager.Roles.ToList();
            ViewBag.RoleId = IDRole;
            ViewBag.ListType = listRole;
            return View("ManageUser", listUser);
        }
    }
}
