using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Soccer_Care.Models;
using System.Data.Entity;
using System.Security.Authentication;
using static System.Reflection.Metadata.BlobBuilder;

namespace Soccer_Care.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Partner")]
    public class PitchController : Controller
    {

        private readonly SoccerCareDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        public PitchController(SoccerCareDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            football.IDUserOwner = _userManager.FindByEmailAsync(football.Username).Result.Id;
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
            _context.listFields.RemoveRange(pitch.ListField);
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
            ViewBag.GetType = _context.TypeFields.ToList();
            ViewBag.ListField = _context.listFields.Where(i => i.IDFootballField == id).ToList();   
            return View(pitch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FootBallFieldModel footBall)
        {
            try
            {
                _context.FootBallFields.Update(footBall);
                _context.SaveChanges();
                return RedirectToAction("ManagePitch","Admin");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult EditChildField(string id)
        {
            var find = _context.listFields.FirstOrDefault(i => i.IDField == id);
            if (find != null) return PartialView("EditChildField", find);
            return NotFound();
        }

        [HttpPost]
        public IActionResult ConfirmEditChildField(ListFieldModel listField)
        {
            TempData["EditSuccess"] = "Đã cập nhật thành công";
            _context.listFields.Update(listField);
            _context.SaveChangesAsync();
            return RedirectToAction("ManagePitch", "Admin", TempData["EditSuccess"]);
        }
        public IActionResult DeleteChildField(string id)
        {
            var find = _context.listFields.FirstOrDefault(i => i.IDField == id);
            if (find != null) return PartialView("DeleteChildField", find);
            return NotFound();
        }
        [HttpPost]
        public IActionResult ConfirmDeleteChildField(string id)
        {
            var find = _context.listFields.FirstOrDefault(i => i.IDField == id);
            if (find != null)
            {
                var findParent = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == find.IDFootballField);
                _context.listFields.Remove(find);
                _context.SaveChangesAsync();
                return RedirectToAction("ManagePitch", "Admin", TempData["EditSuccess"]);
            }
            return NotFound();
        }
    }
}
