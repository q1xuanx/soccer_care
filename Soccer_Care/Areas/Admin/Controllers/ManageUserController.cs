using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;

namespace Soccer_Care.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageUserController : Controller
    {

        SoccerCareDbContext _context;
        IWebHostEnvironment webHostEnvironment;
        public ManageUserController(SoccerCareDbContext _soccer , IWebHostEnvironment webHostEnvironment)
        {
            _context = _soccer;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult EditUser(string username)
        {
            var find = _context.User.FirstOrDefault(i => i.Username.Equals(username));
            ViewBag.ListRole = _context.Role.ToList();
            if (find == null)
            {
                return NotFound();
            }
            return View(find);
        }
        [HttpPost]
        public IActionResult ConfirmEditUser(UserModel user, IFormFile NewImage)
        {
            //if (session.GetString("User") == null)
            //{
              //  return RedirectToAction("Index", "Home");
            //}
            if (NewImage != null)
            {
                var getPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string extension = Path.GetExtension(NewImage.FileName);
                var saveImg = Path.Combine(getPath, user.Username + extension);
                using (var fileStream = new FileStream(saveImg, FileMode.Create))
                {
                    NewImage.CopyTo(fileStream);
                    user.AvatarURL = user.Username + extension;
                }
            }
            _context.User.Update(user);
            _context.SaveChangesAsync();
            return RedirectToAction("ManageUser", "Admin");
        }
    }
}
