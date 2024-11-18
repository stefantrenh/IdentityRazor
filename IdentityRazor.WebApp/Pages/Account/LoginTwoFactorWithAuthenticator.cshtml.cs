using IdentityRazor.WebApp.Data.Account;
using IdentityRazor.WebApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityRazor.WebApp.Pages.Account
{
    public class LoginTwoFactorWithAuthenticatorModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        [BindProperty]
        public AuthenticationMFADTO AuthenticationMFA { get; set; }

        public LoginTwoFactorWithAuthenticatorModel(SignInManager<User> signInManager)
        {
            this.AuthenticationMFA = new AuthenticationMFADTO();
            this.signInManager = signInManager;
        }
        public void OnGet(bool remeberMe)
        {
            this.AuthenticationMFA.SecurityCode = string.Empty;
            this.AuthenticationMFA.RemeberMe = remeberMe;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await signInManager.TwoFactorAuthenticatorSignInAsync(
                                this.AuthenticationMFA.SecurityCode,
                                this.AuthenticationMFA.RemeberMe,
                                false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Authenticator2FA", "You are locked out.");
                }
                else
                {
                    ModelState.AddModelError("Authenticator2FA", "Failed to login.");
                }

                return Page();
            }
        }
    }
}
