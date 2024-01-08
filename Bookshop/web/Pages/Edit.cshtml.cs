using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private BookManager _bookMan;
        private BLL.ReviewManager _reviewManager;
        public EditModel(IBookRepository Brepo, IReviewRepository Rrepo, BLL.ReviewManager reviewManager)
        {
            _bookMan = new BookManager(Brepo, Rrepo);
            _reviewManager =  reviewManager;
        }
        public Review SelectedReview { get; set; }
        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                int reviewId = Convert.ToInt32(HttpContext.Session.GetInt32("reviewId"));
                SelectedReview = _reviewManager.GetReview(reviewId);
                return Page();
            }
            TempData["isLogged"] = "You do not have permission for that operation.";
            return RedirectToPage("/Login");
        }
        public IActionResult OnPost()
        {
            SelectedReview.Id = Convert.ToInt32(HttpContext.Session.GetInt32("reviewId"));
            _reviewManager.UpdateReview(SelectedReview);
            HttpContext.Session.Remove("reviewId");
            return RedirectToPage("/Profile");
        }
        public IActionResult OnPostDelete()
        {
            SelectedReview.Id = Convert.ToInt32(HttpContext.Session.GetInt32("reviewId"));
            _reviewManager.DeleteReview(SelectedReview);
            HttpContext.Session.Remove("reviewId");
            return RedirectToPage("/Profile");
        }
    }
}
