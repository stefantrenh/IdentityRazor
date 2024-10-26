﻿using System.ComponentModel.DataAnnotations;

namespace IdentityRazor.IdentityRazor.Domain
{
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
