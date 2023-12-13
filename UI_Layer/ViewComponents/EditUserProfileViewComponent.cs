using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UI_Layer.Areas.BackendSystem.Models;

namespace UI_Layer.ViewComponents
{
    public class EditUserProfileViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
