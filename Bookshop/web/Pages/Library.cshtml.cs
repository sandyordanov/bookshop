using BLL;
using Classes;
using DAL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace web.Pages
{
    public class LibraryModel : PageModel
    {
        private UserController _userCon;
        private BookManagement _bookManager;

        public List<Book> recommendedBooks { get; set; }
        public LibraryModel(IReviewRepository reviewRepository, IUserRepository userRepository, IBookRepository bookRepo)
        {

            _userCon = new UserController(userRepository);
            _bookManager = new BookManagement(bookRepo, reviewRepository);
        }
        public void OnGet()
        {
            //Recommended
            RecommendationEngine engine = new RecommendationEngine(_bookManager.GetAllBooks(), _userCon.GetAllUsers());
            recommendedBooks = engine.GetRecommendations(Convert.ToInt32(User.FindFirstValue("id")));

            
        }
    }
}
