using BLL;
using Classes;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace web.Pages
{
    public class UserProfileUpdateModel : PageModel
    {
        private readonly UserController _userController;
        [BindProperty]
        public User OldUser { get; set; }

        public UserProfileUpdateModel(IUserRepository userRepo)
        {
            _userController = new UserController(userRepo);
        }
        public void OnGet()
        {
            OldUser = _userController.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {

            }
            _userController.UpdateUserProfile(OldUser);
            return RedirectToPage("/Profile");
        }
    }
}
