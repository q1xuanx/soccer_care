using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;

namespace Soccer_Care.Controllers
{
    public class UserController : Controller
    {
        SoccerCareDbContext scb;
        ISession session;
        IWebHostEnvironment webHostEnvironment;
        public UserController(SoccerCareDbContext scb, IHttpContextAccessor contextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            this.scb = scb;
            session = contextAccessor.HttpContext.Session;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmLogin(UserModel user)
        {
            var find = scb.User.FirstOrDefault(i => i.Username.Equals(user.Username) && i.Password.Equals(user.Password));
            if (find != null)
            {
                if (find.IDRole.Equals("R00"))
                {
                    session.SetString("SuccessLogin", "Đăng nhập thành công");
                    session.SetString("User", find.Username);
                    session.SetString("Role", find.IDRole);
                    return RedirectToAction("Index", "Home");
                }else
                {
                    //To do them tinh nang chuyen huong cho doi tac
                    return NotFound();
                }
            }
            else
            {
                ViewBag.Error = "Sai tên tài khoản hoặc mật khẩu";
                TempData["LoginFail"] = "Đăng nhập thất bại";
                return View("Login");
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ConfirmRegister(UserModel user, String passConfirm)
        {
            user.IDRole = "R00";
            //Guid.NewGuid().ToString(); 
            if (!user.Password.Equals(passConfirm)) {
                ViewBag.Error = "Password nhập lại không khớp";
                TempData["FailRegister"] = "Đăng ký thất bại";
                return View("Register");
            }
            scb.User.Add(user);
            scb.SaveChanges();
            TempData["SuccessRegister"] = "Đăng ký thành công";
            return View("Login");
        }
        public IActionResult LogOut()
        {
            session.Remove("User");
            session.Remove("Role");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult EditUser(string Username)
        {
            var getUser = scb.User.FirstOrDefault(x => x.Username.Equals(Username));
            if (getUser != null)
            {
                return View(getUser);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ConfirmEdit(UserModel user, IFormFile NewImage)
        {
            if (session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (NewImage != null) {
                var getPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string extension = Path.GetExtension(NewImage.FileName);
                var saveImg = Path.Combine(getPath, user.Username + extension);
                using (var fileStream = new FileStream(saveImg, FileMode.Create)) {
                    NewImage.CopyTo(fileStream);
                    user.AvatarURL = user.Username + extension;
                }
            }
            scb.User.Update(user);
            scb.SaveChangesAsync();
            session.SetString("SuccessEdit", "Thay đổi thông tin thành công");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
