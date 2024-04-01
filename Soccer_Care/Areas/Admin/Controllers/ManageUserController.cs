using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Soccer_Care.Models;

namespace Soccer_Care.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageUserController : Controller
    {

        SoccerCareDbContext _context;
        IWebHostEnvironment webHostEnvironment;
        UserManager<UserModel> _userManager;
        RoleManager<IdentityRole> _roleManager;
        public ManageUserController(SoccerCareDbContext _soccer , IWebHostEnvironment webHostEnvironment, UserManager<UserModel> userManager ,RoleManager<IdentityRole> roleManager)
        {
            _context = _soccer;
            this.webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> EditUser(string Id)
        {
            List<IdentityRole> listRole = _roleManager.Roles.ToList();
            var findUser = await _userManager.FindByIdAsync(Id);
            string role = "";
            foreach(IdentityRole r in listRole)
            {
                if (await _userManager.IsInRoleAsync(findUser, r.Name))
                {
                    role = r.Name;
                    break;
                }
            }
            ViewBag.ListRole = listRole;
            ViewBag.CurrentRole = role;
            if (findUser == null)
            {
                return NotFound();
            }
            return View(findUser);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmEditUser(UserModel user, IFormFile NewImage, string role)
        {
            var findUser = _userManager.FindByIdAsync(user.Id);
            if (NewImage != null)
            {
                var getPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string extension = Path.GetExtension(NewImage.FileName);
                var saveImg = Path.Combine(getPath, user.Id + extension);
                using (var fileStream = new FileStream(saveImg, FileMode.Create))
                {
                    NewImage.CopyTo(fileStream);
                    findUser.Result.AvatarURL = user.Id + extension;
                }
            }
            findUser.Result.FullName = user.FullName;
            findUser.Result.PhoneNumber = user.PhoneNumber;
            findUser.Result.IsBlock = user.IsBlock;
            var result = await _userManager.UpdateAsync(findUser.Result);
            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(findUser.Result);
                await _userManager.RemoveFromRolesAsync(findUser.Result, userRoles);
                await _userManager.AddToRoleAsync(findUser.Result, role);
                return RedirectToAction("ManageUser", "Admin");
            }
            
            return NotFound();
        }
    }
}
