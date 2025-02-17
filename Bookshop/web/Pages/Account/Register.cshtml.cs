using BLL;
using Classes;
using DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User registrator { get; set; }

        private UserController controller;

        public RegisterModel(IUserRepository userRepo)
        {
            controller = new UserController(userRepo);
        }
        public void OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                RedirectToPage("AnotherPage");
            }
        }
        public IActionResult OnPost()
        {
            bool success = controller.RegisterUser(registrator);

            if (success)
            {
                return RedirectToPage("/Account/Login");
            }

            TempData["fail"] = "Registration unsuccessful. Username is in use.";
            return Page();
        }

    }
}
