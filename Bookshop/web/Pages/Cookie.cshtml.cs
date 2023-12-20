using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class CookieModel : PageModel
    {
        public IActionResult OnPostSetLightMode()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };
            Response.Cookies.Append("ColorThemeCookie", "light", cookieOptions);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostSetDarkMode()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };
            Response.Cookies.Append("ColorThemeCookie", "dark", cookieOptions);
            return RedirectToPage("Index");
        }
    }
}

