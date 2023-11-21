using BLL;
using Classes;
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


        public IndexModel(IBookRepository repos)
        {

            bookManager = new BookManagement(repos);
        }

        public void OnGet()
        {
            books = bookManager.GetAllBooks();
        }
        public IActionResult OnPost(string id)
        {
                HttpContext.Session.SetString("id", id);
                return RedirectToPage("/Book");
        }
    }
}