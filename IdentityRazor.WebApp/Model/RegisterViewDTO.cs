using System.ComponentModel.DataAnnotations;

namespace IdentityRazor.WebApp.Model
{
    public class RegisterViewDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(dataType:DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Department { get; set; } = string.Empty;
        [Required]
        public string Position { get; set; } = string.Empty;
    }
}
