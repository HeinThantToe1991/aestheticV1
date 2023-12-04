using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Data.IdentityModel
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            //base.Id = Guid.NewGuid();
            ////Default Create
            //CreatedDate = DateTime.Now;
        }

        [Column(TypeName = "varchar(25)")]
        public string FullName { get; set; }

        [ForeignKey("CreatedUserId")]
        public Guid? CreatedUserId { get; set; }

        public ApplicationUser CreatedUser { get; set; } // Self-referencing relationship


        [ScaffoldColumn(false)]
        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public Guid UpdatedUserId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [ScaffoldColumn(false)]
        public bool Active { get; set; }
        public bool SystemUser { get; set; }
    }
}
