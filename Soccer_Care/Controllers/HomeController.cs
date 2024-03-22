using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;
using System.Diagnostics;

namespace Soccer_Care.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IHttpContextAccessor contextAccessor;
        SoccerCareDbContext context;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, SoccerCareDbContext context)
        {
            _logger = logger;
            this.contextAccessor = contextAccessor;
            this.context = context;
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
            //To do se redirect sau... 
            string price1 = price.Split("-")[0];
            string price2 = price.Split("-")[0];
            string address = city + district; 
            var list = context.FootBallFields.Where(i => i.Address.Contains(address) && i.Gia >= int.Parse(price1) && i.Gia <= int.Parse(price2)).ToList();
            return RedirectToAction("Index", "Home");
        }
    }
}