using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace web.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity == null && User.Identity.IsAuthenticated == false)
            {
                TempData["ErrorMessage"] = "Cannot logout if you have are not already logged in";
                RedirectToPage("/Index");
            }
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "Logged out succesfully";
            return RedirectToPage("/Index");
        }
    }
}
