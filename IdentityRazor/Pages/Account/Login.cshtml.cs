using IdentityRazor.IdentityRazor.Application.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace IdentityRazor.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public CredentialDTO Credential { get; set; } = new CredentialDTO();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            if (Credential.UserName == "admin" && Credential.Password == "password") 
            {
                /*Provider*/

                var claims = new List<Claim> {
                    new Claim (ClaimTypes.Name, "admin") ,
                    new Claim (ClaimTypes.Email, "admin@website.com"),
                    new Claim ("Department", "HR"),
                    new Claim ("Admin", "true"),
                    new Claim ("Manager", "true"),
                    new Claim ("EmploymentDate", "2024-05-05")
                };

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };

                var identity = new ClaimsIdentity (claims, "MyCookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal (identity);

               await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

                return RedirectToPage("/Index");

            }

            return Page();
        }
    }
}
