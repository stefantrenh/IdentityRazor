using IdentityRazor.WebApp.Data.Account;
using IdentityRazor.WebApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace IdentityRazor.WebApp.Pages.Account
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public UserProfileViewDTO UserProfile { get; set; }

        [BindProperty]
        public string? SuccessMessage { get; set; }
        public UserProfileModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.UserProfile = new UserProfileViewDTO();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            this.SuccessMessage = string.Empty;

            var (user, departmentClaim, positionClaim) = await GetUserInfoAsync();
            if (user != null)
            {
                this.UserProfile.Email = User.Identity?.Name ?? string.Empty;
                this.UserProfile.Department = departmentClaim?.Value ?? string.Empty;
                this.UserProfile.Position = positionClaim?.Value ?? string.Empty;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (user, departmentClaim, positionClaim) = await GetUserInfoAsync();

            try
            {
                if (user != null && departmentClaim != null)
                {
                    await userManager.ReplaceClaimAsync(user, departmentClaim, new Claim(departmentClaim.Type, UserProfile.Department));
                }
                if (user != null && positionClaim != null)
                {
                    await userManager.ReplaceClaimAsync(user, positionClaim, new Claim(positionClaim.Type, UserProfile.Position));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("UserProfile", "Error occured during updating user profile.");
            }

            this.SuccessMessage = "The user profile is updated successfully";

            return Page();
        }

        private async Task<(User? user, Claim? departmentClaim, Claim? positionClaim)> GetUserInfoAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity?.Name ?? string.Empty);
            if (user != null)
            {
                var claims = await userManager.GetClaimsAsync(user);
                var departmentClaim = claims.FirstOrDefault(x => x.Type == "Department");
                var positionClaim = claims.FirstOrDefault(x => x.Type == "Position");

                return (user, departmentClaim, positionClaim);
            }
            else
            {
                return (null,null,null);
            }
        }
    }

}
