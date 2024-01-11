using BLL;
using Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class AllBooksModel : PageModel
    {
        private readonly BookManager bookManager;
        public List<Book> Books { get; set; }
        public AllBooksModel(BookManager bookMan)
        {
            bookManager = bookMan;
        }
        public void OnGet()
        {
            Books = bookManager.GetAllBooks();
        }
    }
}
