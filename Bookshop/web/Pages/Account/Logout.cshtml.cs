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
        public IActionResult OnPostAsync()
        {
            if (User.Identity == null && User.Identity.IsAuthenticated == false)
            {
                TempData["Message"] = "You are already logged in! Log out if you want to login a new profile.";
                RedirectToPage("/Index");
            }
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
