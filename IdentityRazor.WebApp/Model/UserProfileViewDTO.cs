using System.ComponentModel.DataAnnotations;

namespace IdentityRazor.WebApp.Model
{
    public class UserProfileViewDTO
    {
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Department { get; set; } = string.Empty;
        [Required]
        public string Position { get; set; } = string.Empty;
    }
}
