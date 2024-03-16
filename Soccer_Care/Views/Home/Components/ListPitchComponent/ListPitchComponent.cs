using Microsoft.AspNetCore.Mvc;

namespace Soccer_Care.Views.Home.Components.ListPitchComponent
{
    public class ListPitchComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("ListPitchComponent");
        }
    }
}
