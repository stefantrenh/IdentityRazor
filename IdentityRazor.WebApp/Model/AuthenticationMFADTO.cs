using System.ComponentModel.DataAnnotations;

namespace IdentityRazor.WebApp.Model
{
    public class AuthenticationMFADTO
    {
        [Required]
        [Display(Name = "Code")]
        public string SecurityCode { get; set; } = string.Empty;

        public bool RemeberMe { get; set; }
    }
}
