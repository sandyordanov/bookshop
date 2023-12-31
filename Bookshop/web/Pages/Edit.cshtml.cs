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
        private BookManagement _bookMan;
        public EditModel(IBookRepository Brepo, IReviewRepository Rrepo)
        {
            _bookMan = new BookManagement(Brepo, Rrepo);
        }
        public Review SelectedReview { get; set; }
        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                int reviewId = Convert.ToInt32(HttpContext.Session.GetInt32("reviewId"));
                SelectedReview = _bookMan.GetReview(reviewId);
                return Page();
            }
            TempData["isLogged"] = "You do not have permission for that operation.";
            return RedirectToPage("/Login");
        }
        public IActionResult OnPost()
        {
            SelectedReview.Id = Convert.ToInt32(HttpContext.Session.GetInt32("reviewId"));
            _bookMan.UpdateReview(SelectedReview);
            HttpContext.Session.Remove("reviewId");
            return RedirectToPage("/Profile");
        }
    }
}
