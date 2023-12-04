using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using UI_Layer.Globalizer;

namespace HomerUI.Areas.BackendSystem.Controllers
{
    [Area("BackendSystem")]
    public class AdminHomeController : Controller
    {
        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult TestPage()
        {
            return View();
        }
    }
}
