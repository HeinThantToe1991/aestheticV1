using Microsoft.AspNetCore.Mvc;

namespace UI_Layer.ViewComponents
{
    public class Pannel6ViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var myTask = await Task.FromResult(GetViewData());
            //return View(myTask);
            return View();
        }
    }
}
