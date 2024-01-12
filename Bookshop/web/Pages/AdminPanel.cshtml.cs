using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    [Authorize(Policy = "PowerUser")]
    public class AdminPanelModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
