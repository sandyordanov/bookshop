using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class LibraryModel : PageModel
    {
        public void OnGet()
        {
            Response.Cookies.Append("MyCookie", "value1");
        }
    }
}
