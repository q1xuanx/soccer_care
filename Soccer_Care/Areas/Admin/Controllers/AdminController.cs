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
                List<DetailsOrderModel> details = _context.DetailsOrder.ToList();
                foreach(DetailsOrderModel detailsOrder in details)
                {
                    detailsOrder.Order = _context.OrderField.FirstOrDefault(i => i.IDOrder == detailsOrder.IDOrder);
                    detailsOrder.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == detailsOrder.Order.IDFootballField);
                }
                List<string> listOrder = details.Where(i => i.DateTime == DateTime.Today && i.Order.FootBall.IDUserOwner == gerCurrUser.Id && (i.isThanhToan == 1 || i.Status == "Đã Đến")).Select(i => i.IDOrder).ToList();
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
        
        public async Task<List<object>> GetTotalsOfMonth()
        {
            List<object> list = new List<object>();
            List<string> dayOfMonth = new List<string>();
            var gerCurrUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var getRoleOfUser = await _userManager.IsInRoleAsync(gerCurrUser, "Admin");
            int endDayOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int month = DateTime.Now.Month;
            string months = "";
            if (month % 10 == month)
            {
                months = "0" + month.ToString();
            } else months = month.ToString();
            for (int i = 1; i <= endDayOfMonth; i++)
            {
                if (i % 10 == i)
                {
                    dayOfMonth.Add("0"+i.ToString()+"/"+months+ "/" + DateTime.Now.Year.ToString());
                }
                else dayOfMonth.Add(i.ToString() + "/" + months + "/" + DateTime.Now.Year.ToString());
            }
            list.Add(dayOfMonth);
            List<DetailsOrderModel> listOrder = _context.DetailsOrder.ToList();
            foreach(DetailsOrderModel details in listOrder)
            {
                details.Order = _context.OrderField.FirstOrDefault(i => i.IDOrder == details.IDOrder);
                details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
            }
            if (getRoleOfUser)
            {
                List<double> total = new List<double>();
                foreach (string order in dayOfMonth)
                {
                    var getDays = listOrder.Where(i => i.DateTime.Date.ToString("dd/MM/yyyy") == order && (i.isThanhToan == 1 || i.Status == "Đã Đến"));
                    double tempTotalsOfDay = 0;
                    if (getDays != null)
                    {
                        foreach(DetailsOrderModel details in getDays)
                        {
                            var getIDOrder = _context.OrderField.FirstOrDefault(i => i.IDOrder == details.IDOrder);
                            if (getIDOrder != null)
                            {
                                getIDOrder.ListField = _context.listFields.FirstOrDefault(i => i.IDField == getIDOrder.IDChildField);
                                tempTotalsOfDay += getIDOrder.ListField.Gia * 1.5;
                            }
                        }
                        total.Add(tempTotalsOfDay);
                    }
                }
                list.Add(total);
            }
            else
            {
                List<double> total = new List<double>();
                foreach (string order in dayOfMonth)
                {
                    var getDays = listOrder.Where(i => i.DateTime.Date.ToShortDateString() == order && i.isThanhToan == 1 || i.Status == "Đã Đến" && i.Order.FootBall.IDUserOwner == gerCurrUser.Id);
                    double tempTotalsOfDay = 0;
                    if (getDays != null)
                    {
                        foreach (DetailsOrderModel details in getDays)
                        {
                            var getIDOrder = _context.OrderField.FirstOrDefault(i => i.IDOrder == details.IDOrder);
                            if (getIDOrder != null)
                            {
                                getIDOrder.ListField = _context.listFields.FirstOrDefault(i => i.IDField == getIDOrder.IDChildField);
                                tempTotalsOfDay += getIDOrder.ListField.Gia * 1.5;
                            }
                        }
                        total.Add(tempTotalsOfDay);
                    }
                }
                list.Add(total);
            }
            return list; 
        }
        public IActionResult GetExactDate(DateTime date)
        {
            if (date.ToString("dd/MM/yyyy") == "01/01/0001")
            {
                var findz = _context.DetailsOrder.ToList();
                if (User.IsInRole("Admin"))
                {
                    foreach (DetailsOrderModel details in findz)
                    {
                        details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                        details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                        details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                        details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                    }
                    return PartialView("UpdateStatus", findz);
                }
                else
                {
                    foreach (DetailsOrderModel details in findz)
                    {
                        details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                        details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                        details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                        details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                    }
                    var getCurrUser = _userManager.GetUserAsync(HttpContext.User).Result;
                    var getExactField = findz.Where(i => i.Order.FootBall.IDUserOwner == getCurrUser.Id).ToList();
                    return PartialView("UpdateStatus", getExactField);
                }
                
            }
            var find = _context.DetailsOrder.Where(i => i.DateTime == date).ToList();
            if (User.IsInRole("Admin"))
            {
                foreach (DetailsOrderModel details in find)
                {
                    details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                    details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                    details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                    details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                }
                return PartialView("UpdateStatus", find);
            }
            else
            {
                foreach (DetailsOrderModel details in find)
                {
                    details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                    details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                    details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                    details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                }
                var getCurrUser = _userManager.GetUserAsync(HttpContext.User).Result;
                var getExactField = find.Where(i => i.Order.FootBall.IDUserOwner == getCurrUser.Id).ToList();
                return PartialView("UpdateStatus", getExactField);
            }
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
                if (User.IsInRole("Admin"))
                {

                    foreach (DetailsOrderModel details in find)
                    {
                        details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                        details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                        details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                        details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                    }
                }
                else
                {
                    foreach (DetailsOrderModel details in find)
                    {
                        details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                        details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                        details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                        details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                    }
                    var getCurrUser = _userManager.GetUserAsync(HttpContext.User).Result;
                    var getExactField = find.Where(i => i.Order.FootBall.IDUserOwner == getCurrUser.Id).ToList();
                }
                return PartialView("UpdateStatus", find);
            }
            return NotFound();
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
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
            else
            {
                var find = _context.DetailsOrder.ToList();
                foreach (DetailsOrderModel details in find)
                {
                    details.Order = _context.OrderField.Where(i => i.IDOrder == details.IDOrder).FirstOrDefault();
                    details.Order.User = _context.Users.Where(i => i.Id == details.Order.IDUser).FirstOrDefault();
                    details.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == details.Order.IDFootballField);
                    details.Order.ListField = _context.listFields.FirstOrDefault(i => i.IDField == details.Order.IDChildField);
                }
                var getCurrUser = _userManager.GetUserAsync(HttpContext.User).Result;
                var getExactField = find.Where(i => i.Order.FootBall.IDUserOwner == getCurrUser.Id).ToList();
                return View(getExactField);
            }
        }
        public async Task<IActionResult> ManagePitch()
        {
            var getCurUser = await _userManager.GetUserAsync(HttpContext.User);
            if (User.IsInRole("Admin"))
            {
                var listPitch = _context.FootBallFields.Where(i => i.isDisable == 0).ToList();
                return View(listPitch);
            }
            var listField = _context.FootBallFields.Where(i => i.isDisable == 0 && i.IDUserOwner == getCurUser.Id);
            return View(listField);
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
