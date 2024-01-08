using UI_Layer.Models;

namespace UI_Layer.Areas.BackendSystem.Models
{
    public class DoctorViewModel : BaseEntityViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string DivisionId { get; set; }
        public string DistrictId { get; set; }
        public string TownshipId { get; set; }
        public string TownshipName { get; set; }
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
    }
}
