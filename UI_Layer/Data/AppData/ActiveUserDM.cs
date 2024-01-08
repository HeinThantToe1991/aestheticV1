using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI_Layer.Data.AppData
{
    public class ActiveUserDM : EntityBase
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        [Column(TypeName = "char(36)")]
        public Guid UserId { get; set; }
        [Column(TypeName = "char(1)")]
        public string UserType { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ConnectionId { get; set; }

    }
}
