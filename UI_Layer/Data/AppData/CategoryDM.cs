using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI_Layer.Data.AppData
{
    [Table("Category")]
    public class CategoryDM : EntityBase
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string Code { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }
    }
}
