using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI_Layer.Data.IdentityModel
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
    }
}
