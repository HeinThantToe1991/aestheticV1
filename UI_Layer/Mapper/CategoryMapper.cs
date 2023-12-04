using Microsoft.AspNetCore.Identity;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data.AppData;
using UI_Layer.Data.IdentityModel;

namespace UI_Layer.Mapper
{
    public class CategoryMapper
    {
        public static async Task<CategoryViewModel> DataModelToViewModel(CategoryDM dataModel, UserManager<ApplicationUser> userManager)
        {
            var returnModel = new CategoryViewModel();
            returnModel.Id = dataModel.Id.ToString();
            returnModel.Code = dataModel.Code;
            returnModel.Description = dataModel.Description;
            returnModel.CreatedDate = dataModel.CreatedDate;
            returnModel.CreatedDate_String = dataModel.CreatedDate.ToString("dd-MM-yyyy");
            returnModel.CreatedUserId = dataModel.CreatedUserId.ToString();
            var userInfo =await Globalizer.CommonDBMethod.GetApplicationAsync(userManager, returnModel.CreatedUserId);
            returnModel.CreatedUserName = userInfo.FullName;
            if (dataModel.UpdatedUserId != null)
            {
                returnModel.UpdatedUserId = dataModel.UpdatedUserId.ToString();
                userInfo = await Globalizer.CommonDBMethod.GetApplicationAsync(userManager, dataModel.UpdatedUserId.ToString());
                returnModel.UpdatedUserName = userInfo.FullName;
                returnModel.UpdatedDate_String = Convert.ToDateTime(dataModel.UpdatedDate).ToString("dd-MM-yyyy");              
            }
            returnModel.Active = dataModel.Active;
            returnModel.SystemDefault = dataModel.SystemDefault;      
            return returnModel;
        }

        public static async Task<List<CategoryViewModel>> DataModelToViewModel_List (List<CategoryDM> dataModel, UserManager<ApplicationUser> userManager)
        {
            var returnModelList = new List<CategoryViewModel>();
            foreach (var item in dataModel) {
                var returnModel = new CategoryViewModel();
                returnModel.Id = item.Id.ToString();
                returnModel.Code = item.Code;
                returnModel.Description = item.Description;
                returnModel.CreatedDate = item.CreatedDate;
                returnModel.CreatedDate_String = item.CreatedDate.ToString("dd-MM-yyyy");
                returnModel.CreatedUserId = item.CreatedUserId.ToString();
                var userInfo = await Globalizer.CommonDBMethod.GetApplicationAsync(userManager, returnModel.CreatedUserId);
                returnModel.CreatedUserName = userInfo.FullName;
                if (item.UpdatedUserId != null)
                {
                    returnModel.UpdatedUserId = item.UpdatedUserId.ToString();
                    userInfo = await Globalizer.CommonDBMethod.GetApplicationAsync(userManager, item.UpdatedUserId.ToString());
                    returnModel.UpdatedUserName = userInfo.FullName;
                    returnModel.UpdatedDate_String = Convert.ToDateTime(item.UpdatedDate).ToString("dd-MM-yyyy");
                }
                returnModel.Active = item.Active;
                returnModel.SystemDefault = item.SystemDefault;
                returnModelList.Add(returnModel);
            }
            return returnModelList;
        }
        public static CategoryDM ViewModelToDataModel(CategoryViewModel viewModel)
        {
            var returnModel = new CategoryDM();
            returnModel.Id = Guid.Parse(viewModel.Id);
            returnModel.Code = viewModel.Code;
            returnModel.Description = viewModel.Description;
            returnModel.CreatedDate = viewModel.CreatedDate;
            returnModel.CreatedUserId = Guid.Parse(viewModel.CreatedUserId);
            returnModel.UpdatedDate = viewModel.UpdatedDate;
            returnModel.UpdatedUserId = Guid.Parse(viewModel.UpdatedUserId);
            returnModel.Active = viewModel.Active;
            returnModel.SystemDefault = viewModel.SystemDefault;
            return returnModel;
        }
    }
}
