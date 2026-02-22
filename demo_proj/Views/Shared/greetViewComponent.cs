using Microsoft.AspNetCore.Mvc;

namespace demo_proj.Views.Shared
{
    public class greetViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name = "javor")
        {
            return View("Default", name);
        }

    }
}
