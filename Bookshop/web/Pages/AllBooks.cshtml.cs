using BLL;
using BLL.StrategyFilters;
using Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class AllBooksModel : PageModel
    {
        private readonly BookManager bookManager;
        public List<Author> AllAuthors { get; set; }
        public List<Book> Books { get; set; }
        [BindProperty]
        public string Booktype { get; set; }
        [BindProperty]
        public int? SelectedAuthorId { get; set; }
        public AllBooksModel(BookManager bookMan)
        {
            bookManager = bookMan;
        }
        public IActionResult OnGet(int? SelectedAuthorId, string Booktype)
        {
            AllAuthors = bookManager.GetAllAuthors();
            Books = bookManager.GetAllBooks();
            List<IBookFilterStrategy> strategies = new List<IBookFilterStrategy>();

            var authorIds = new List<int>();
            if (SelectedAuthorId != null)
            {
                Author selectedAuthor = AllAuthors.FirstOrDefault(x => x.Id == SelectedAuthorId);
                AuthorFilterStrategy authorFilter = new AuthorFilterStrategy(selectedAuthor);

                strategies.Add(authorFilter);
            }

            // Use trim to remove leading and trailing whitespace
            if (!string.IsNullOrEmpty(Booktype))
            {
                var cleanedBooktype = Booktype.Trim();
                if (cleanedBooktype.Equals("Paperbook", StringComparison.OrdinalIgnoreCase))
                {
                    PaperBookFilterStrategy paperBookFilter = new PaperBookFilterStrategy();
                    strategies.Add(paperBookFilter);
                }
                else if (cleanedBooktype.Equals("Ebook", StringComparison.OrdinalIgnoreCase))
                {
                    EbookFilterStrategy ebookFilter = new EbookFilterStrategy();
                    strategies.Add(ebookFilter);
                }
            }

            Books = bookManager.FilterByStrategies(strategies).ToList();
            return Page();
        }


    }
}
