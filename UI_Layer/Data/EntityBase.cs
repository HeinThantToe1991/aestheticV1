using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Data
{
    public class EntityBase
    {
        [ScaffoldColumn(false)]
        public Guid CreatedUserId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public Guid? UpdatedUserId { get; set; }


        [ScaffoldColumn(false)]
        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        public bool SystemDefault { get; set; }
    }
}
