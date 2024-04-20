using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;
using System.Diagnostics;

namespace Soccer_Care.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ISession session;
        SoccerCareDbContext _context;
        UserManager<UserModel> _userManager; 
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, SoccerCareDbContext context, UserManager<UserModel> userManager, IHttpContextAccessor session)
        {
            _logger = logger;
            this._context = context;
            _userManager = userManager;
            this.session = session.HttpContext.Session;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Intro()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search(string city, string district, string price)
        {
            string price1 = price.Split("-")[0];
            string price2 = "9999999";
            if (price.Split("-")[1] != null)
            {
                price2 = price.Split("-")[1];
            }
            string address = district + ",TP." + cast(city);
            session.SetString("price1", price1);
            session.SetString("price2", price2);
            session.SetString("address", address);
            return RedirectToAction("Index", "PitchBall");
        }
        public string cast(string s)
        {
            String res = s[0].ToString();
            for(int i = 1; i < s.Length - 1; i++)
            {
                if (s[i] == ' ') res += s[i + 1].ToString();
            }
            return res; 
        }
    }
}