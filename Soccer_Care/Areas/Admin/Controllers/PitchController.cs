using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Soccer_Care.Models;
using System.Collections.Generic;
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
        IWebHostEnvironment env;
        public PitchController(SoccerCareDbContext context, UserManager<UserModel> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            this.env = env;
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
        public async Task<IActionResult> Create(FootBallFieldModel football, string city, string district, string street, IFormFile ImageFile)
        {
            string address = street + "," + district + "," + city;
            football.IDUserOwner = _userManager.FindByEmailAsync(football.Username).Result.Id;
            football.Address = address;
            football.IDFootBallField = Guid.NewGuid().ToString();
            if (ImageFile != null)
            {
                var folderPath = Path.Combine(env.WebRootPath, "images");
                var getExtension = Path.GetExtension(ImageFile.FileName);
                var ImagePath = Path.Combine(folderPath, football.IDFootBallField + getExtension);
                using (var fileStream = new FileStream(ImagePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                    football.HinhAnhDaiDien = football.IDFootBallField + getExtension;
                }
            }else
            {
                football.HinhAnhDaiDien = "no_img";
            }
            if(!ModelState.IsValid)
            {
                float minPrice = 9999999;
                foreach (ListFieldModel list in football.ListField){
                    list.IDField = Guid.NewGuid().ToString();
                    list.IDFootballField = football.IDFootBallField;
                    if (list.Gia < minPrice) minPrice = list.Gia;
                    if (list.FileImage != null)
                    {
                        var FolderPath = Path.Combine(env.WebRootPath, "images");
                        var GetExtension = Path.GetExtension(list.FileImage.FileName);
                        var ImagePath = Path.Combine(FolderPath, list.IDField + GetExtension);
                        using (var fileStream = new FileStream(ImagePath, FileMode.Create))
                        {
                            list.FileImage.CopyTo(fileStream);
                            list.HinhAnhSanBong = list.IDField + GetExtension;
                        }
                    }
                    else
                    {
                        list.HinhAnhSanBong = "no_img";
                    }
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
            pitch.isDisable = 1;
            _context.FootBallFields.Update(pitch);
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
        public ActionResult Edit(FootBallFieldModel footBall, IFormFile ImageFile)
        {
            try
            {
                var editField = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == footBall.IDFootBallField);
                if (ImageFile != null)
                {
                    var FolderPath = Path.Combine(env.WebRootPath, "images");
                    var GetExtension = Path.GetExtension(ImageFile.FileName);
                    var ImagePath = Path.Combine(FolderPath, editField.IDFootBallField + GetExtension);
                    using (var fileStream = new FileStream(ImagePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(fileStream);
                        editField.HinhAnhDaiDien = editField.IDFootBallField + GetExtension;
                    }
                }
                editField.Address = footBall.Address;
                editField.Gia = footBall.Gia; 
                editField.Name = footBall.Name;
                _context.FootBallFields.Update(editField);
                _context.SaveChanges();
                return RedirectToAction("ManagePitch","Admin");
            }
            catch
            {
                return View();
            }
        }
        //Mini field
        public IActionResult EditChildField(string id)
        {
            var find = _context.listFields.FirstOrDefault(i => i.IDField == id);
            if (find != null) return PartialView("EditChildField", find);
            return NotFound();
        }

        [HttpPost]
        public IActionResult ConfirmEditChildField(ListFieldModel listField, IFormFile ImageFile)
        {
            TempData["EditSuccess"] = "Đã cập nhật thành công";
            var getImg = _context.listFields.FirstOrDefault(i => i.IDField == listField.IDField);
            if (ImageFile != null)
            {
                var FolderPath = Path.Combine(env.WebRootPath, "images");
                var GetExtension = Path.GetExtension(ImageFile.FileName);
                var ImagePath = Path.Combine(FolderPath, listField.IDField + GetExtension);
                using (var fileStream = new FileStream(ImagePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                    listField.HinhAnhSanBong = listField.IDField + GetExtension;
                }
            }else
            {
                listField.HinhAnhSanBong = getImg.HinhAnhSanBong;
            }
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
                find.isDisable = 1;
                _context.listFields.Update(find);
                _context.SaveChangesAsync();
                return RedirectToAction("ManagePitch", "Admin", TempData["EditSuccess"]);
            }
            return NotFound();
        }
    }
}
