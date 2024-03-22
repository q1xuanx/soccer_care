using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Soccer_Care.Models;
using System.Data.Entity;
using static System.Reflection.Metadata.BlobBuilder;

namespace Soccer_Care.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PitchController : Controller
    {

        private readonly SoccerCareDbContext _context;

        public PitchController(SoccerCareDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TypeField = JsonConvert.SerializeObject(_context.TypeFields.ToList());
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FootBallFieldModel football, string city, string district, string street)
        {
            string address = street + "," + district + "," + city;
            football.Address = address;
            football.IDFootBallField = Guid.NewGuid().ToString();
            if(!ModelState.IsValid)
            {
                float minPrice = 9999999;
                foreach (ListFieldModel list in football.ListField){
                    list.IDField = Guid.NewGuid().ToString();
                    list.IDFootballField = football.IDFootBallField;
                    if (list.Gia < minPrice) minPrice = list.Gia;
                    _context.listFields.Add(list);                   
                }
                football.Gia = minPrice;
                _context.FootBallFields.Add(football);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Admin");
            }
            return View(football);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pitch = _context.FootBallFields.FirstOrDefault(u => id == u.IDFootBallField);
            if (pitch == null)
            {
                return NotFound();
            }
            return View(pitch);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var pitch = await _context.FootBallFields.FindAsync(id);
            _context.FootBallFields.Remove(pitch);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManagePitch","Admin");
        }

        public ActionResult Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var pitch = _context.FootBallFields.FirstOrDefault(u => id == u.IDFootBallField);
            if (pitch == null)
            {
                return NotFound();
            }
            ViewBag.Context = _context;
            ViewBag.ListField = _context.listFields.Where(i => i.IDFootballField == id).ToList();   
            return View(pitch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, FootBallFieldModel footBall)
        {
            try
            {
                var f = _context.FootBallFields.FirstOrDefault(b => b.IDFootBallField == id);
                f.Name = footBall.Name;
                f.Address = footBall.Address;
                f.Gia = footBall.Gia;
                f.Username = footBall.Username;
                _context.SaveChanges();
                return RedirectToAction("ManagePitch","Admin");
            }
            catch
            {
                return View();
            }
        }
    }
}
