using UI_Layer.Models;

namespace UI_Layer.Areas.BackendSystem.Models
{
    public class CustomerViewModel : BaseEntityViewModel
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public string? CustomerName { get; set; }
        public string FullName { get; set; }
        public string DivisionId { get; set; }
        public string DistrictId { get; set; }
        public string TownshipId { get; set; }
        public string TownshipName { get; set; }
        public string CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Remark { get; set; }
        public string DeleteRemark { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string DateOfBirthStr { get; set; }

        public string UserImageStr { get; set; }
        public IFormFile UserImage { get; set; }
        public int Day { get; set; }
        public int Month { get; set; } 
        public int Year { get; set; }
        public bool IsValid { get; private set; }

        public string LogInUserStatus {get; set; }
        //public CustomerViewModel(string id, string userId, string customerName, string divisionId, string districtId, string townshipId, string townshipName, string customerTypeId, string customerTypeName, string email, string phoneNo, string gender, string remark, string deleteRemark, string address, DateTime dateOfBirth, string dateOfBirthStr, string userImageStr, IFormFile userImage, int day, int month, int year)
        //{
        //    Id = id;
        //    UserId = userId;
        //    CustomerName = customerName;
        //    DivisionId = divisionId;
        //    DistrictId = districtId;
        //    TownshipId = townshipId;
        //    TownshipName = townshipName;
        //    CustomerTypeId = customerTypeId;
        //    CustomerTypeName = customerTypeName;
        //    Email = email;
        //    PhoneNo = phoneNo;
        //    Gender = gender;
        //    Remark = remark;
        //    DeleteRemark = deleteRemark;
        //    Address = address;
        //    DateOfBirth = dateOfBirth;
        //    DateOfBirthStr = dateOfBirthStr;
        //    UserImageStr = userImageStr;
        //    UserImage = userImage;
        //    Day = day;
        //    Month = month;
        //    Year = year;

        //    Validate();
        //}

        //private void Validate()
        //{
        //    IsValid = !string.IsNullOrEmpty(CustomerName) &&   // CustomerName is required
        //              !string.IsNullOrEmpty(CustomerTypeId) &&  // CustomerTypeId is required
        //              !string.IsNullOrEmpty(Email) &&           // Email is required
        //              !string.IsNullOrEmpty(PhoneNo) &&         // PhoneNo is required
        //              !string.IsNullOrEmpty(Gender) &&          // Gender is required
        //              !string.IsNullOrEmpty(Address) &&         // Address is required
        //              !string.IsNullOrEmpty(DateOfBirthStr) &&  // DateOfBirth_str is required
        //              //IsValidEmail(Email) &&                   // Validate email format
        //              DateOfBirth >= new DateTime(1900, 1, 1) && DateOfBirth <= DateTime.Now && // DateOfBirth is within a valid range
        //              Day >= 1 && Day <= 31 &&                 // Day is within a valid range
        //              Month >= 1 && Month <= 12 &&             // Month is within a valid range
        //              Year >= 1900 && Year <= DateTime.Now.Year;  // Year is within a valid range

        //    // Additional validation for minimum and maximum lengths
        //    IsValid = IsValid &&
        //              CustomerName.Length >= 1 && CustomerName.Length <= 100 &&    // Example min and max lengths
        //              Address.Length >= 1 && Address.Length <= 200;
        //}
    }
}
