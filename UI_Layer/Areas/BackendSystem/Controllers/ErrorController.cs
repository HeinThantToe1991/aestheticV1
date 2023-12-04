using Microsoft.AspNetCore.Mvc;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class ErrorController : Controller
    {
        public IActionResult PermissionError()
        {
            return View();
        }
        public IActionResult FileNotFoundError()
        {
            return View();
        }

        public IActionResult NeedLogInError()
        {
            return View();
        }
    }
}
