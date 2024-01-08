using Microsoft.AspNetCore.Mvc;
using UI_Layer.Globalizer;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class DashboardController : Controller
    {
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
