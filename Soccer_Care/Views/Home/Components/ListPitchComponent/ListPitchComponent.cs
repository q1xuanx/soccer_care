using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;
using System.Data.Entity;

namespace Soccer_Care.Views.Home.Components.ListPitchComponent
{
    public class ListPitchComponent : ViewComponent
    {
        private readonly SoccerCareDbContext _context;

        public ListPitchComponent(SoccerCareDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var pitch =  _context.FootBallFields.ToList();
            ViewBag.ListPitch = _context.listFields.ToList();
            return View("ListPitchComponent", pitch);
        }
    }
}
