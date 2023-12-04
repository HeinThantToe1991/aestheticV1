using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI_Layer.Data.AppData
{
    [Table("ServiceStock")]
    public class ServiceStockDM : EntityBase
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(5)")]
        public required string Code { get; set; }
        [Column(TypeName = "varchar(50)")]
        public required string Description { get; set; }
    }
}
