using System.Security.Claims;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data.AppData;
namespace UI_Layer.Mapper
{
    public static class CompanyInformationMapper
    {
        public static CompanyInformationViewModel DataModelToViewModel(CompanyInformationDM dataModel)
        {
            var returnModel = new CompanyInformationViewModel();
            returnModel.Id = dataModel.Id.ToString();
            returnModel.CompanyName = dataModel.CompanyName;
            returnModel.CompanyShortName = dataModel.CompanyShortName;
            returnModel.RefCompanyId = dataModel.RefCompanyId;
            returnModel.CompanyAddress = dataModel.CompanyAddress;
            returnModel.ContactPerson = dataModel.ContactPerson;
            returnModel.PhoneNumber = dataModel.PhoneNumber;
            returnModel.WebsiteLink = dataModel.WebsiteLink;
            returnModel.FacebookLink = dataModel.FacebookLink;
            returnModel.CompanyLogo_str = dataModel.CompanyLogo;
            returnModel.Email = dataModel.Email;
            returnModel.District = dataModel.District;
            returnModel.Email = dataModel.Email;
            returnModel.StateDivision = dataModel.StateDivision;
            returnModel.Township = dataModel.Township;
            returnModel.StreetNumber_Name = dataModel.StreetNumber_Name;
            returnModel.BusinessType = dataModel.BusinessType;
            return returnModel;
        }
        public static CompanyInformationDM ViewModelToDataModel(CompanyInformationViewModel viewModel)
        {
            var returnModel = new CompanyInformationDM();
            returnModel.Id = Guid.Parse(viewModel.Id);
            returnModel.RefCompanyId = viewModel.RefCompanyId;
            returnModel.CompanyName = viewModel.CompanyName;
            returnModel.CompanyShortName = viewModel.CompanyShortName;
            returnModel.CompanyAddress = viewModel.CompanyAddress;
            returnModel.ContactPerson = viewModel.ContactPerson;
            returnModel.PhoneNumber = viewModel.PhoneNumber;
            returnModel.WebsiteLink = viewModel.WebsiteLink;
            returnModel.FacebookLink = viewModel.FacebookLink;
            returnModel.CompanyLogo = viewModel.CompanyLogo_str;
            returnModel.Email = viewModel.Email;
            returnModel.District = viewModel.District;
            returnModel.StateDivision = viewModel.StateDivision;
            returnModel.Township = viewModel.Township;
            returnModel.Active = true;
            returnModel.CreatedDate = viewModel.CreatedDate;
            returnModel.CreatedUserId = Guid.Parse(viewModel.CreatedUserId);
            returnModel.BusinessType = viewModel.BusinessType;
            returnModel.StreetNumber_Name = viewModel.StreetNumber_Name;
            return returnModel;
        }

    }
}
