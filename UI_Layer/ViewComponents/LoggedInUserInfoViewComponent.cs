using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UI_Layer.Areas.BackendSystem.Models;

namespace UI_Layer.ViewComponents
{
    public class LoggedInUserInfoViewComponent : ViewComponent
    {
    

        private readonly IDistributedCache _cache;

        public LoggedInUserInfoViewComponent(IDistributedCache cache)
        {
            _cache = cache;
          
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var existingUserJson = await _cache.GetStringAsync("LoggedInUser");
            var loggedInfoModel= JsonConvert.DeserializeObject<CustomerViewModel>(existingUserJson);
            return View(loggedInfoModel);
        }

    }
}
