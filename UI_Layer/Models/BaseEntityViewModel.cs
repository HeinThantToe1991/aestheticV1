using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Models
{
    public class BaseEntityViewModel
    {
        public string Id { get; set; }
        public string CreatedUserId { get; set; }

        public string CreatedUserName { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedDate_String { get; set; }


        public string UpdatedUserId { get; set; }

        public string UpdatedUserName { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDate_String { get; set; }
        public bool Active { get; set; }

        public bool SystemDefault { get; set; }
    }
}
