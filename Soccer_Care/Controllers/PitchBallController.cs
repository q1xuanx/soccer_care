using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;

namespace Soccer_Care.Controllers
{
    public class PitchBallController : Controller
    {
        IHttpContextAccessor contextAccessor;
        private readonly SoccerCareDbContext _context;
        public PitchBallController(IHttpContextAccessor contextAccessor, SoccerCareDbContext context) {
            this.contextAccessor = contextAccessor;
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DatSan(string id)
        {
            var pitch = _context.FootBallFields.FirstOrDefault(f => f.IDFootBallField == id);
            return View(pitch);
        }

        public IActionResult DS(string id)
        {
            var listField = _context.listFields.Where(l => l.IDFootballField == id).ToList();
            var pitch = _context.FootBallFields.FirstOrDefault(f => f.IDFootBallField == id);
            string namef = pitch.Name.ToString();
            ViewData["name"] = namef;
            return View(listField);
        }


        public IActionResult DatSanConfirm()
        {

            
            return View("Thanks");
        }
    }
}
