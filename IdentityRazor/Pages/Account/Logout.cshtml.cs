using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityRazor.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private const string CookieAuthScheme = "MyCookieAuth";
        public async Task<IActionResult> OnPost()
        {
            await HttpContext.SignOutAsync(CookieAuthScheme);

            return RedirectToPage("/Index");
        }
    }
}
