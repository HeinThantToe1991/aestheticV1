using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UI_Layer.Models;

namespace UI_Layer.Areas.BackendSystem.Models
{
    public class CompanyInformationViewModel : BaseEntityViewModel
    {
        [MaxLength(100)]
        public string RefCompanyId { get; set; }
        [MaxLength(100)]
        public string CompanyName { get; set; }
        [MaxLength(100)]
        public string CompanyShortName { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [MaxLength(254)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string ContactPerson { get; set; }
        public IFormFile CompanyLogo { get; set; }
        [MaxLength(15)]
        public string StateDivision { get; set; }
        [MaxLength(15)]
        public string District { get; set; }
        [MaxLength(15)]
        public string Township { get; set; }
        [MaxLength(254)]
        public string CompanyAddress { get; set; }
        [MaxLength(200)]
        public string FacebookLink { get; set; }
        [MaxLength(200)]
        public string WebsiteLink { get; set; }
        public string CompanyLogo_str { get; set; }

        [MaxLength(100)]
        public string StreetNumber_Name { get; set; }
        [MaxLength(200)]
        public string BusinessType { get; set; }

    }
}
