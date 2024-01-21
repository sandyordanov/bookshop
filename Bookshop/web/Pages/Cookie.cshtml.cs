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
            if (ThemeColor == "light")
            {
                Response.Cookies.Append("ColorThemeCookie", "dark", cookieOptions);

            }
            else if (ThemeColor == "dark")
            {
                Response.Cookies.Append("ColorThemeCookie", "light", cookieOptions);
            }

            return RedirectToPage("/Index");
        }

    }
}

