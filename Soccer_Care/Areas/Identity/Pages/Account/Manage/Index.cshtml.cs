// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Soccer_Care.Models;

namespace Soccer_Care.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        IWebHostEnvironment env; 
        public IndexModel(
            UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.env = env;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public IFormFile Image { get; set; }
            public string ImagePic { get; set; }
            public string fullName { get;set; }
            public int IsBlock { get; set; }
        }

        private async Task LoadAsync(UserModel user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var findUser = _userManager.FindByEmailAsync(user.Email);
            
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = findUser.Result.PhoneNumber,
                ImagePic = findUser.Result.AvatarURL,
                fullName = findUser.Result.FullName,
                IsBlock = findUser.Result.IsBlock
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
           
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (Input.Image != null)
            {
                var FolderPath = Path.Combine(env.WebRootPath, "images");
                var GetExtension = Path.GetExtension(Input.Image.FileName);
                var ImagePath = Path.Combine(FolderPath, user.Id + GetExtension);
                using (var fileStream = new FileStream(ImagePath, FileMode.Create))
                {
                    Input.Image.CopyTo(fileStream);
                    user.AvatarURL = user.Id + GetExtension;
                }
            }
            else
            {
                var findUser = _userManager.FindByIdAsync(user.Id);
                if (findUser.Result != null)
                {
                    user.AvatarURL = findUser.Result.AvatarURL;
                }
            }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.PhoneNumber = Input.PhoneNumber;
            user.FullName = Input.fullName;
            user.IsBlock = Input.IsBlock;

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
