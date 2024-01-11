using BLL;
using Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class SearchModel : PageModel
    {
        private readonly BookManager bookManager;
        public string Search { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public List<Book> Books { get; set; } = new List<Book>();
        public SearchModel(BookManager bookMan)
        {
            bookManager = bookMan;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSearch(string search)
        {

            if (search == null)
            {
                Search = string.Empty;
                return Page();
            }
            else
            {
                Search = $"'{search}'";
                Books = bookManager.SearchForBooks(search, bookManager.GetAllBooks());
                return Page();
            }

        }
    }
}
