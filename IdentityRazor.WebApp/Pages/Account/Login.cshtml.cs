
using IdentityRazor.WebApp.Data.Account;
using IdentityRazor.WebApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityRazor.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public CredentialViewDTO Credential { get; set; } = new CredentialViewDTO();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await signInManager.PasswordSignInAsync(
                                             Credential.Email,
                                             Credential.Password,
                                             Credential.RememberMe,
                                             false);
            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login.");
                }
            }

            return Page();

        }
    }
}
