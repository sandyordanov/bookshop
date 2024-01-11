using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Net;
using System.Security.Claims;

namespace web.Pages
{
    public class BookModel : PageModel
    {

        private BookManager _bookManager;
        private UserController userController;
        private ReviewManager _reviewManager;

        public List<Review> Reviews { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public Book Book { get; set; }
        [BindProperty]
        public Review NewReview { get; set; }
        [BindProperty(SupportsGet = true)]
        public Statistics Statistics { get; set; }
        public bool UserHasReviews { get; set; }
        public User MyUser { get; set; }
        public BookModel(BookManager bookManager, ReviewManager reviewMan, UserController userCon)
        {
            userController = userCon;
            _bookManager = bookManager;
            _reviewManager = reviewMan;
            NewReview = new Review();
        }
        [BindProperty]
        public bool pagiButtonPressed { get; set; } = false;
        public IActionResult OnGet(int id, int? pageNumber)
        {
            if (pageNumber != null) { pagiButtonPressed = true; }

            try
            {
                const int pageSize = 5;
                CurrentPage = pageNumber ?? 1;

                int totalReviewCount = _reviewManager.GetTotalReviewCountForBook(id);

                TotalPages = (int)Math.Ceiling(totalReviewCount / (double)pageSize);
                Book = _bookManager.GetBook(id);

                Reviews = _reviewManager.GetReviewsPagination(Book, CurrentPage, pageSize);
                Statistics = _bookManager.GetBookStatistics(Book);

                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    int userId = Convert.ToInt32(User.FindFirstValue("id"));
                    MyUser = userController.GetUserLikes(userController.GetUserById(userId));
                    UserHasReviews = _reviewManager.UserHasReviewsOnBook(userId, Book.Id);
                    NewReview = _reviewManager.GetReviewOnBookByUser(MyUser, Book);
                    if (UserHasReviews)
                    {
                        Reviews.Remove(Reviews.FirstOrDefault(x => x.Id == NewReview.Id));
                    }
                }
                return Page();
            }
            catch (Exception x)
            {
                TempData["error"] = x.Message;
                return RedirectToPage("/error");
            }
        }
        public IActionResult OnPost(int bookId)
        {
            if (User.FindFirstValue("id").IsNullOrEmpty())
            {
                @TempData["isLogged"] = "If you want to leave a review, log in first. Don't have an account - register.";
                return RedirectToPage("/Account/Login");
            }

            NewReview.User = userController.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
            var book = _bookManager.GetBook(bookId);
            var review = new Review(0, NewReview.Comment, NewReview.Rating, DateTime.Now, 0, NewReview.User, book);
            _reviewManager.AddReview(review);
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

        public IActionResult OnPostUpvote(int revId)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var rev = _reviewManager.GetReview(revId);
                User currentUser = userController.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
                _reviewManager.VoteOnReview(rev, currentUser, "upVote");
                return RedirectToPage("/Book", new { id = rev.Book.Id });
            }
            return RedirectToPage("Login");
        }
        public IActionResult OnPostDownvote(int revId)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var rev = _reviewManager.GetReview(revId);
                User currentUser = userController.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
                _reviewManager.VoteOnReview(rev, currentUser, "downVote");
                return RedirectToPage("/Book", new { id = rev.Book.Id });
            }
            return RedirectToPage("Login");
        }
        public IActionResult OnPostEdit(int revId)
        {
            HttpContext.Session.SetInt32("reviewId", revId);
            return RedirectToPage("/Edit");
        }
    }
}
