﻿using Microsoft.AspNetCore.Mvc;

namespace UI_Layer.ViewComponents
{
    public class AboutCompanyViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

    }
}
