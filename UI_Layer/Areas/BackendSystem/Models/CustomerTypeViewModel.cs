﻿using System.ComponentModel.DataAnnotations;
using UI_Layer.Models;

namespace UI_Layer.Areas.BackendSystem.Models
{
    public class CategoryViewModel : BaseEntityViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
