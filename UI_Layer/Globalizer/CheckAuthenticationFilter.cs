using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UI_Layer.Globalizer
{
    public class CheckAuthenticationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("NeedLogInError", "Error", new { area = "BackendSystem" });
            }
        }
    }
}
