using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace web.Pages
{
    public class CookieModel : PageModel
    {
        [BindProperty]
        public string ThemeColor { get; set; }
        public IActionResult OnPostSetMode(string value)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };

            Response.Cookies.Append("ColorThemeCookie", ThemeColor, cookieOptions);

            return RedirectToPage("/Index");
        }

    }
}

