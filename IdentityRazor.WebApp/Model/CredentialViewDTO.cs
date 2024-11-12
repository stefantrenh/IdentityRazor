using System.ComponentModel.DataAnnotations;

namespace IdentityRazor.WebApp.Model
{
    public class CredentialViewDTO
    {
        [Required]
        [Display(Name = "User Name")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
