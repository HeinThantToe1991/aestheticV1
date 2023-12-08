using System.Net;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data.AppData;

namespace UI_Layer.Mapper
{
    public static class StaffMapper
    {
        public static StaffDM ViewModelToDataModel(StaffViewModel viewModel)
        {
            var returnModel = new StaffDM();
            returnModel.Id = Guid.Parse(viewModel.Id);
            returnModel.ApplicationUserId = Guid.Parse(viewModel.UserId);
            returnModel.TownshipId = viewModel.TownshipId;
            returnModel.UserImage = viewModel.UserImageStr;
            returnModel.Gender = viewModel.Gender;
            returnModel.Address = viewModel.Address;
            returnModel.DateOfBirth = viewModel.DateOfBirth;
            returnModel.Remark = viewModel.Remark;
            returnModel.DeleteRemark = viewModel.DeleteRemark;
            returnModel.CreatedUserId = Guid.Parse(viewModel.CreatedUserId);
            returnModel.CreatedDate = viewModel.CreatedDate;
          
            if (!string.IsNullOrEmpty(viewModel.UpdatedUserId))
            {
                returnModel.UpdatedDate = viewModel.UpdatedDate;
            }
            if (!string.IsNullOrEmpty(viewModel.UpdatedUserId))
            {
                returnModel.UpdatedUserId = Guid.Parse(viewModel.UpdatedUserId);
            }
           
            returnModel.Active = viewModel.Active;
            return returnModel;
        }
    }
}
