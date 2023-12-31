using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Security.Claims;

namespace web.Pages
{
    public class BookModel : PageModel
    {
        public List<Review> Reviews { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public Book Book { get; set; }
        [BindProperty]
        public Review NewReview { get; set; }
        [BindProperty(SupportsGet = true)]
        public Statistics Statistics { get; set; }
        private BookManagement bookManager;
        public bool UserHasReviews { get; set; }
        public BookModel(IBookRepository bookRepo, IReviewRepository reviewRepo)
        {
            bookManager = new BookManagement(bookRepo, reviewRepo);
            NewReview = new Review();
        }
        [BindProperty]
        public bool pagiButtonPressed { get; set; } = false;
        public IActionResult OnGet(int id, int? pageNumber)
        {
            //try
            //{
            const int pageSize = 5;
            int currentPage = pageNumber ?? 1;

            Reviews = bookManager.GetReviewsPagination(id, currentPage, pageSize);
            int totalReviewCount = bookManager.GetTotalReviewCountForBook(id);

            TotalPages = (int)Math.Ceiling(totalReviewCount / (double)pageSize);
            CurrentPage = currentPage;
            Book = bookManager.GetBookAndReviews(id);
            Statistics = Book.GetStatistics();
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                int userId = Convert.ToInt32(User.FindFirstValue("id"));
                UserHasReviews = bookManager.UserHasReviewsOnBook(userId, Book.Id);
                NewReview = Book.GetReviewByUser(userId);
            }
            return Page();
            //}
            //catch (Exception x)
            //{
            //TempData["error"] = x.Message;
            //return RedirectToPage("/Error");
            //}
        }
        public IActionResult OnPost(int bookId)
        {
            if (User.FindFirstValue("id").IsNullOrEmpty())
            {
                @TempData["isLogged"] = "If you want to leave a review, log in first. Don't have an account - register.";
                return RedirectToPage("/Account/Login");
            }
            UserController man = new UserController();
            NewReview.User = man.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
            var review = new Review(0, NewReview.Comment, NewReview.Rating, DateTime.Now, 0, NewReview.User, bookId);
            bookManager.AddReview(review);
            return RedirectToPage("/Book", new { id = bookId });
        }
        public IActionResult OnPostUserOperations()
        {
            if (User.FindFirstValue("id").IsNullOrEmpty())
            {
                @TempData["isLogged"] = "You need to be logged in for this operation.";
                return RedirectToPage("Account/Login");
            }
            return RedirectToPage("Library");
        }

        public void OnPostUpvote(int revId, int bookId)
        {
            var rev = bookManager.GetReview(revId);
            bookManager.LikeReview(rev);

        }
        public IActionResult OnPostEdit(int revId)
        {
            HttpContext.Session.SetInt32("reviewId", revId);
            return RedirectToPage("/Edit");
        }
    }
}
