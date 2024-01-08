using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI_Layer.Data.AppData
{
    [Table("CompanyInformation")]
    public class CompanyInformationDM : EntityBase
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string RefCompanyId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string CompanyName{ get; set; }
        [Column(TypeName = "varchar(18)")]
        public string CompanyShortName { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(254)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ContactPerson { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CompanyLogo { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string BusinessType { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string StateDivision { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string District { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Township { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string StreetNumber_Name { get; set; }
        [Column(TypeName = "varchar(254)")]
        public string CompanyAddress { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string FacebookLink { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string WebsiteLink { get; set; }

    }
}
