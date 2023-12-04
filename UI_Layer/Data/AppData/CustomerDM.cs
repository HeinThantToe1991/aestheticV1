using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UI_Layer.Data.IdentityModel;

namespace UI_Layer.Data.AppData
{
    [Table("Customer")]
    public class CustomerDM : EntityBase
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid ApplicationUserId { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string TownshipId { get; set; }
        [Column(TypeName = "char(36)")]
        public Guid CustomerTypeId { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateOfBirth { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Remark { get; set; }
        public string DeleteRemark { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string UserImage { get; set; }
        public virtual CustomerTypeDM CustomerType { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
