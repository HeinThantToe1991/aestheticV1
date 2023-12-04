using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using UI_Layer.Resources;
using System.Collections.Generic;
namespace UI_Layer.Areas.BackendSystem.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "MV00015")]
        [Display(Name = "UserName")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "MV00014")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

    }
}
