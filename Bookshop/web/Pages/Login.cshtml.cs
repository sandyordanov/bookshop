using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BLL;
using Classes;

namespace web.Pages
{
    public class LoginModel : PageModel
    {
        private UserServices userManager;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }


        public LoginModel()
        {
                userManager = new UserServices();
        }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "You are already logged in! Log out if you want to login a new profile.";
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password))
            {
                User loggee = userManager.GetUser(Username);
                if (loggee != null)
                {
                    if (loggee.Password == Password)
                    {
                        List<Claim> claims = new List<Claim>();
                       // claims.Add(new Claim("id", loggee.GetId().ToString()));
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                        return RedirectToPage("/Index");
                    }
                }
            }
            TempData["IsLogged"] = "Username or password invalid.";
            return Page();
        }
    }
}
