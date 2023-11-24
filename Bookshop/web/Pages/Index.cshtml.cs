using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;

namespace web.Pages
{
    public class IndexModel : PageModel
    {
        private BookManagement bookManager;


        [BindProperty(SupportsGet = true)]
        public List<Book> books { get; set; }


        public IndexModel(IBookRepository bookRepo, IReviewRepository reviewRepo)
        {
            bookManager = new BookManagement(bookRepo, reviewRepo);
        }

        public void OnGet()
        {
            books = bookManager.GetAllBooks();
        }
        public IActionResult OnPost(int? id)
        {      
                return RedirectToPage("/Book",id);
        }
    }
}