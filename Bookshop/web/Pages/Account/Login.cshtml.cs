using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BLL;
using Classes;
using DAL;

namespace web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private UserController userManager;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }


        public LoginModel(IUserRepository userRepo)
        {
            userManager = new UserController(userRepo);
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
            if (!ModelState.IsValid) { return Page(); }
            if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password))
            {
                int result = 0;
                try
                {
                    result = userManager.TryToLogUserIn(Username, Password);
                }
                catch (Exception)
                {
                    TempData["isLogged"] = "Username or password invalid.";
                }

                if (result > 0)
                {
                    //poweruser check
                    if (userManager.CheckIfUserIsPowerUser(result))
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("id", result.ToString()));
                        claims.Add(new Claim("UserType", "PowerUser"));
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                    }
                    else
                    {
                        List<Claim> claims = new List<Claim>();

                        claims.Add(new Claim("id", result.ToString()));
                        claims.Add(new Claim("UserType", "User"));
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

                    }
                    return RedirectToPage("/Index");
                }
            }
            TempData["IsLogged"] = "Username or password invalid.";
            return Page();
        }
    }
}
