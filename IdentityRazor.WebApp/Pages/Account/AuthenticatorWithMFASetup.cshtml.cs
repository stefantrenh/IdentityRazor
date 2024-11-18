using IdentityRazor.WebApp.Data.Account;
using IdentityRazor.WebApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QRCoder;

namespace IdentityRazor.WebApp.Pages.Account
{
    [Authorize]
    public class AuthenticatorWithMFASetupModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public SetUpMFAViewDTO SetUpMFA { get; set; }
        [BindProperty]
        public bool Succeeded { get; set; }

        public AuthenticatorWithMFASetupModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.SetUpMFA = new SetUpMFAViewDTO();
            this.Succeeded = false;
        }
        public async Task OnGetAsync()
        {
            var user = await userManager.GetUserAsync(base.User);
            if (user != null)
            {
                var key = await userManager.GetAuthenticatorKeyAsync(user);

                if (string.IsNullOrEmpty(key))
                {
                    await userManager.ResetAuthenticatorKeyAsync(user);
                    key = await userManager.GetAuthenticatorKeyAsync(user);
                }

                this.SetUpMFA.Key = key ?? string.Empty;
                this.SetUpMFA.QRCodeBytes = GenerateQRCodeBytes(
                                            "My web app",
                                             this.SetUpMFA.Key,
                                             user.Email ?? string.Empty);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(base.User);
            if (user != null && await userManager.VerifyTwoFactorTokenAsync(
                                                    user,
                                                    userManager.Options.Tokens.AuthenticatorTokenProvider,
                                                    this.SetUpMFA.SecurityCode
                ))
            {
                await userManager.SetTwoFactorEnabledAsync(user, true);
                this.Succeeded = true;
            }
            else
            {
                ModelState.AddModelError("AuthenticatorSetup", "Something went wrong with the authenticator setup.");
            }

            return Page();
        }

        private Byte[] GenerateQRCodeBytes(string provider, string key, string userEmail)
        {
            var qrCodeGenerator = new QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(
                $"otpauth://totp/{provider}:{userEmail}?secret={key}&issuer={provider}",
                QRCodeGenerator.ECCLevel.Q);

            var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }
    }
}
