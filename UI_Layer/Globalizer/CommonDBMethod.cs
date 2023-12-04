using Microsoft.AspNetCore.Identity;
using UI_Layer.Data;
using UI_Layer.Data.IdentityModel;

namespace UI_Layer.Globalizer
{
    public static class CommonDBMethod
    {
        public async static Task<ApplicationUser> GetApplicationAsync(UserManager<ApplicationUser> userManager,string id)
        {
            return await userManager.FindByIdAsync(id);

        }
    }
}
