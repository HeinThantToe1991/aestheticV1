using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data.IdentityModel;
using UI_Layer.Data;
using UI_Layer.Globalizer;
using UI_Layer.Models;
using UI_Layer.Resources;

namespace UI_Layer.Areas.BackendSystem.Controllers
{
    public class GeneralController : Controller
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public GeneralController(IStringLocalizer<Resource> localizer, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _localizer = localizer;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public IActionResult DeleteConfirm(DeleteConfirmViewModel deleteConfirmViewModel)
        {
            try
            {
                return PartialView("_DeleteConfirm", deleteConfirmViewModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        
        }

        [TypeFilter(typeof(CheckAuthenticationFilter))]
        public async Task<ActionResult> Delete(DeleteConfirmViewModel deleteConfirmViewModel)
        {
            var id = deleteConfirmViewModel.ItemId;
            ReturnMessageViewModel<DeleteConfirmViewModel> data = new ReturnMessageViewModel<DeleteConfirmViewModel>();
            try
            {
                switch (deleteConfirmViewModel.ItemName)
                {
                    case "Customer":
                        var entity = _dbContext.Customer.FirstOrDefault(s => s.Id.ToString().Equals(id));
                        _dbContext.Remove(entity);
                        break;
                }
                var transaction = await _dbContext.SaveChangesAsync();
                if (transaction == 1)
                {
                    data.MessageStatus = MessageStatus.Success;
                    data.Success = true;
                    data.Message = _localizer["MI00005"].ToString();
                }
            }
            catch (Exception ex)
            {
                data.MessageStatus = MessageStatus.Info;
                data.Success = false;
                data.Message = Globalizer.CommonMethods.GetInnerException(ex, _localizer["MI00023"].ToString());
            }
            return Json(data);
        }
    }
}
