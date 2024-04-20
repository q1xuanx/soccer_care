using Blazored.Toast.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Soccer_Care.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

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

        [HttpGet]
        public async Task<List<object>> GetTotals()
        {
            var gerCurrUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var getRoleOfUser = await _userManager.IsInRoleAsync(gerCurrUser, "Admin");
            string currentDay = DateTime.Now.ToString("dd/MM/yyyy");
            List<object> list = new List<object>();
            list.Add(currentDay);
            if (getRoleOfUser)
            {
                List<string> listOrder = _context.DetailsOrder.Where(i => i.DateTime == DateTime.Today && i.isThanhToan == 1 || i.Status == "Đã Đến").Select(i => i.IDOrder).ToList();
                double total = 0;
                foreach (string order in listOrder)
                {
                    var findOrder = _context.OrderField.FirstOrDefault(i => i.IDOrder == order);
                    findOrder.ListField = _context.listFields.FirstOrDefault(i => i.IDField == findOrder.IDChildField );
                    total += findOrder.ListField.Gia * 1.5;
                }
                list.Add(total);
            }
            else
            {
                List<string> listOrder = _context.DetailsOrder.Where(i => i.DateTime == DateTime.Today && i.Order.IDUser == gerCurrUser.Id && i.isThanhToan == 1 || i.Status == "Đã Đến").Select(i => i.IDOrder).ToList();
                double total = 0;
                foreach(string order in listOrder)
                {
                    var findOrder = _context.OrderField.FirstOrDefault(i => i.IDOrder == order);
                    findOrder.ListField = _context.listFields.FirstOrDefault(i => i.IDField == findOrder.IDChildField);
                    total += findOrder.ListField.Gia * 1.5;
                }
                list.Add(total);
            }
            return list;
        }
        public IActionResult UpdateStatus(String id, String value)
        {
            var getOrder = _context.DetailsOrder.FirstOrDefault(i => i.IDOrder == id); 
            if(getOrder != null)
            {
                getOrder.Status = value;
                _context.DetailsOrder.Update(getOrder);
                _context.SaveChanges();
                var find = _context.DetailsOrder.ToList();
                foreach (DetailsOrderModel details in find)
                {
                    details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                    details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                    details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                    details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                }
                return PartialView("UpdateStatus", find);
            }
            return NotFound();
        }
        public IActionResult Index()
        {
            var find = _context.DetailsOrder.ToList();
            foreach (DetailsOrderModel details in find)
            {
                details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
            }
                  
            return View(find);
        }
        public IActionResult ManagePitch()
        {
            var listPitch = _context.FootBallFields.Where(i => i.isDisable == 0).ToList();
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
