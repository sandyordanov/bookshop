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
        private UserController userManager;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }


        public LoginModel()
        {
            userManager = new UserController();
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

                var result = userManager.TryToLogUserIn(Username, Password);
                if (result > 0)
                {
                    List<Claim> claims = new List<Claim>();
                    
                    claims.Add(new Claim("id", result.ToString()));
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                    return RedirectToPage("/Index");
                }
            }
            TempData["IsLogged"] = "Username or password invalid.";
            return Page();
        }
        public async Task<IActionResult> RedirectAfterRegistration(User registrated)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("id", registrated.Id.ToString()));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            return RedirectToPage("/Index");
        }
    }
}
