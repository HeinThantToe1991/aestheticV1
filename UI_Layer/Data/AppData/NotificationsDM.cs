using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI_Layer.Data.AppData
{
    [Table("Notifications")]
    public class NotificationsDM : EntityBase
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid ApplicationUserId { get; set; }

        [Required(ErrorMessage = "MessageText is required")]
        [Column(TypeName = "nvarchar(250)")]
        public  string MessageText { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required(ErrorMessage = "MessageType is required")]
        public  string MessageType { get; set; }

    }
}
