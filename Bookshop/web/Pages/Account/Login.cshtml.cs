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
            if(!ModelState.IsValid) { return Page(); }
            if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(Password))
            {
                //admin auth
                if (Username == "admin" && Password == "admin")
                {
                    List<Claim> claims = new List<Claim>();

                    claims.Add(new Claim("UserType", "Admin"));
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

                    return RedirectToPage("/AdminPanel");
                }

                var result = userManager.TryToLogUserIn(Username, Password);
                if (result > 0)
                {
                    List<Claim> claims = new List<Claim>();

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
        public async Task<IActionResult> OnGetRedirectAfterRegistration(User registrated)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, registrated.Name));
            claims.Add(new Claim("id", registrated.Id.ToString()));
            claims.Add(new Claim("UserType", "User"));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }

    }
}
