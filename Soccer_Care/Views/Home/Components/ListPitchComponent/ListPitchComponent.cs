using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;
using System.Data.Entity;

namespace Soccer_Care.Views.Home.Components.ListPitchComponent
{
    public class ListPitchComponent : ViewComponent
    {
        private readonly SoccerCareDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public ListPitchComponent(SoccerCareDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var pitch =  _context.FootBallFields.Where(i=> i.isDisable == 0).ToList();
            foreach(FootBallFieldModel field in pitch)
            {
                field.ListField = _context.listFields.Where(i => i.IDFootballField == field.IDFootBallField).ToList();
                field.ratings = _context.Ratings.Where(i => i.IDField == field.IDFootBallField).ToList();
            }
            var getUserId = _userManager.GetUserAsync(HttpContext.User);
            if (getUserId.Result == null) return View("ListPitchComponent", pitch);
            ViewBag.ListFieldLike = _context.FieldLike.Where(i => i.Username == getUserId.Result.Id).ToList();
            return View("ListPitchComponent", pitch);
        }
    }
}
