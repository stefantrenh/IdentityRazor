using IdentityRazor.WebApp.Data.Account;
using IdentityRazor.WebApp.Model;
using IdentityRazor.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace IdentityRazor.WebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;

        public RegisterModel(UserManager<User> userManager, IEmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }

        [BindProperty]
        public RegisterViewDTO RegisterView { get; set; } = new RegisterViewDTO();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            var user = new User
            {
                Email = RegisterView.Email,
                UserName = RegisterView.Email,
            };

            var claimDepartment = new Claim("Department", RegisterView.Department);
            var claimPosition = new Claim("Position", RegisterView.Position);

            var result = await this.userManager.CreateAsync(user, RegisterView.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddClaimAsync(user, claimDepartment);
                await this.userManager.AddClaimAsync(user, claimPosition);

                var confirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                return Redirect(Url.PageLink(pageName: "/Account/ConfirmEmail",
                             values: new { userId = user.Id, token = confirmationToken }) ?? "");


                //Mail server to send emails using sendinblue. Not this is not focused on high code quality just testing the functions.
                //Should store password on enviroment variables.

                //var conformationLink = Redirect(Url.PageLink(pageName: "/Account/ConfirmEmail",
                //             values: new { userId = user.Id, token = confirmationToken }) ?? "");

                //await emailService.SendAsync("fromEmail",
                //                             user.Email,
                //                             "Please confirm your email",
                //                             $"Plese click on this link to confirm your email address: {conformationLink}");

                //return RedirectToPage("/Account/Login");

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return Page();
            }
        }
    }

}
