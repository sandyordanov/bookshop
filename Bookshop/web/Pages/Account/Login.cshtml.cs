using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BLL;
using Classes;

namespace web.Pages.Account
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
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "You are already logged in! Log out if you want to login a new profile.";
                RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if(!ModelState.IsValid) { return Page(); }
            if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password))
            {

                var result = userManager.TryToLogUserIn(Username, Password);
                if (result > 0)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, userManager.GetUserById(result).Username));
                    claims.Add(new Claim("id", result.ToString()));
                    claims.Add(new Claim("UserType", "User"));
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

                    return RedirectToPage("/Index");
                }
            }
            TempData["IsLogged"] = "Username or password invalid.";
            return Page();
        }
        public IActionResult OnGetRedirectAfterRegistration(User registrated)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, registrated.Name));
            claims.Add(new Claim("id", registrated.Id.ToString()));
            claims.Add(new Claim("UserType", "User"));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }
    }
}
