using Microsoft.AspNetCore.Mvc;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class ServiceStockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
