using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityRazor.Pages.Department
{
    [Authorize (Policy = "HRManager")]
    public class HRManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
