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
        private BookManager _bookManager;
        private BLL.ReviewManager _reviewManager;

        public List<Book> recommendedBooks { get; set; }
        public LibraryModel(IReviewRepository reviewRepository, IUserRepository userRepository, IBookRepository bookRepo, BLL.ReviewManager revMan)
        {
            _reviewManager = revMan;
            _userCon = new UserController(userRepository);
            _bookManager = new BookManager(bookRepo, reviewRepository);
        }
        public void OnGet()
        {
            //Recommended
            List<User> users = _userCon.GetAllUsers();
            foreach (var user in users)
            {
                   user.Reviews = (_reviewManager.GetAllReviewsByUser(user));
            }
            RecommendationEngine engine = new RecommendationEngine(_bookManager.GetAllBooksWithReviews(), users);
            //try
            //{
                recommendedBooks = engine.GetRecommendations(Convert.ToInt32(User.FindFirstValue("id")));
            //}
            //catch (Exception ex)
            //{
            //    TempData["recommendationErrors"] = ex.Message;
            //}
            

            
        }
    }
}
