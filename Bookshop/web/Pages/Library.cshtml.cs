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
        public LibraryModel(IReviewRepository reviewRepository, IUserRepository userRepository, IBookRepository bookRepo, ReviewManager revMan)
        {
            _reviewManager = revMan;
            _userCon = new UserController(userRepository);
            _bookManager = new BookManager(bookRepo, reviewRepository);
        }
        public void OnGet()
        {
            //Recommended
            Dictionary<Book, List<Review>> books = new Dictionary<Book, List<Review>>();
            Dictionary<User, List<Review>> users = new Dictionary<User, List<Review>>();
            
            var allUsers = _userCon.GetAllUsers();
            var allBooks = _bookManager.GetAllBooks();
            foreach (var book in allBooks)
            {
                var reviews = _reviewManager.GetAllReviewsByBook(book);
                books.Add(book, reviews);
            }
            foreach (var user in allUsers)
            {
                var reviews = _reviewManager.GetAllReviewsByUser(user);
                users.Add(user, reviews);
            }
            RecommendationEngine engine = new RecommendationEngine(books, users);
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
