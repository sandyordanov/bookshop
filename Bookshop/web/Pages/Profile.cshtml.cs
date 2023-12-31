using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace web.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        UserController _userCon;
        [BindProperty]
        public BookManagement _bookManager { get; set; }
        public List<Review> MyReviews { get; set; }
        public List<Book> recommendedBooks { get; set; }
        public ProfileModel(IReviewRepository reviewRepository, IUserRepository userRepository, IBookRepository bookRepo)
        {

            _userCon = new UserController();
            _bookManager = new BookManagement(bookRepo, reviewRepository);
        }
        public IActionResult OnGet()
        {
            if (User.Identity == null && User.Identity.IsAuthenticated == false)
            {
                TempData["Message"] = "You are already logged in! Log out if you want to login a new profile.";
                RedirectToPage("AnotherPage");
            }
            //Recommended
            RecommendationEngine engine = new RecommendationEngine(_bookManager.GetAllBooks(), _userCon.GetAllUsers());
            recommendedBooks = engine.GetRecommendations(Convert.ToInt32(User.FindFirstValue("id")));

            MyReviews = _bookManager.GetAllReviewsByUser(Convert.ToInt32(User.FindFirstValue("id")));
            return Page();
        }
    }
}
