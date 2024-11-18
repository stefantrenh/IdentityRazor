using System.ComponentModel.DataAnnotations;

namespace IdentityRazor.WebApp.Model
{
    public class SetUpMFAViewDTO
    {
        public string? Key { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string SecurityCode { get; set; } = string.Empty;

        public Byte[]? QRCodeBytes { get; set; }
    }
}
