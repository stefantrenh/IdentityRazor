using IdentityRazor.WebApp.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityRazor.WebApp.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public string Message { get; set; } = string.Empty;

        public ConfirmEmailModel(UserManager<User> user)
        {
            this.userManager = user;
        }
        public async Task<IActionResult> OnGetAsync(string userId , string token)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            if (user != null) 
            {
                var result = await this.userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded) 
                {
                    this.Message = "Email address is confirm, you can now try to login.";
                    return Page();
                }
            }

            this.Message = "Failed to validate email";
            return Page();
        }
    }
}
