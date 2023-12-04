using Microsoft.AspNetCore.Mvc;

namespace UI_Layer.Controllers
{
    public class NovaController : Controller
    {
        public IActionResult NovaHome()
        {
            return View();
        }

    }
}
