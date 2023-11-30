using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace web.Pages
{
    public class BookModel : PageModel
    {
        public Book Book { get; set; }
        [BindProperty]
        public Review NewReview { get; set; }
        [BindProperty(SupportsGet = true)]
        public Statistics Statistics { get; set; }
        private BookManagement bookManager;
        public BookModel(IBookRepository bookRepo, IReviewRepository reviewRepo)
        {
            bookManager = new BookManagement(bookRepo, reviewRepo);
            NewReview = new Review();
        }
        public IActionResult OnGet(int id)
        {
            try
            {
                Book = bookManager.GetBookAndReviews(id);
                Statistics = Book.GetStatistics();
                return Page();
            }
            catch (Exception x)
            {
                TempData["error"] = x.Message;
                return RedirectToPage("/Error");
            }
           
        }
        public IActionResult OnPost(int bookId)
        {
            UserController man = new UserController();
            NewReview.User = man.GetUserById(Convert.ToInt32(User.FindFirstValue("id")));
            var review = new Review(0, NewReview.Comment, NewReview.Rating, DateTime.Now, 0, NewReview.User, bookId);
            bookManager.AddReview(review);
            return RedirectToPage("/Book", new { id = bookId });
        }
    }
}
