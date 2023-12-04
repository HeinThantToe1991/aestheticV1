using System.Net;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data.AppData;

namespace UI_Layer.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerViewModel DataModelToViewModel(CustomerDM dataModel)
        {
            var returnModel = new CustomerViewModel();
            returnModel.Id =dataModel.Id.ToString();
            returnModel.UserId = dataModel.ApplicationUserId.ToString();
            returnModel.TownshipId = dataModel.TownshipId;
            returnModel.CustomerTypeId = dataModel.CustomerTypeId.ToString();
            //returnModel.UserImage = dataModel.UserImageStr;
            returnModel.Gender = dataModel.Gender;
            returnModel.Address = dataModel.Address;
            returnModel.DateOfBirth = dataModel.DateOfBirth;
            returnModel.Remark = dataModel.Remark;
            returnModel.DeleteRemark = dataModel.DeleteRemark;
            returnModel.CreatedUserId = dataModel.CreatedUserId.ToString();
            returnModel.CreatedDate = dataModel.CreatedDate;
            returnModel.UpdatedDate = dataModel.UpdatedDate;
            returnModel.UpdatedUserId = dataModel.UpdatedUserId.ToString();
            returnModel.Active = dataModel.Active;
            return returnModel;
        }
        public static CustomerDM ViewModelToDataModel(CustomerViewModel viewModel)
        {
            var returnModel = new CustomerDM();
            returnModel.Id = Guid.Parse(viewModel.Id);
            returnModel.ApplicationUserId = Guid.Parse(viewModel.UserId);
            returnModel.TownshipId = viewModel.TownshipId;
            returnModel.CustomerTypeId = Guid.Parse(viewModel.CustomerTypeId);
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
