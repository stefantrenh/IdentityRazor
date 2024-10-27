using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityRazor.Pages.Department
{
    [Authorize(Policy = "SysAdmin")]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
