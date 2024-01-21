using BLL;
using Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    [BindProperties(SupportsGet = true)]
    public class ContributeModel : PageModel
    {
        private readonly BookManager _bookManager;

        public Book Book { get; set; }

        public int Pages { get; set; }
        public List<Author> AllAuthors { get; set; } // Populate this list with available authors

        public List<int> SelectedAuthors { get; set; } // Holds the selected author IDs
        public string ISBN { get; set; }
        public string ISBN10 { get; set; }
        public double Filesize { get; set; }
        public string DownloadLink { get; set; }

        public ContributeModel(BookManager bookManager)
        {
            _bookManager = bookManager;
        }
        public void OnGet()
        {
            AllAuthors = _bookManager.GetAllAuthors();
        }

        public IActionResult OnPostBook()
        {
          
            var errorMessages = new List<string>();
            try
            {
                var paperBook = new PaperBook(Book.Id, Book.Title, Book.Description, Book.Publisher, Book.Language,
                 Book.PublicationDate, Book.Format, GetAuthorsByIds(SelectedAuthors), Pages, ISBN, ISBN10);

                _bookManager.AddNewBook(paperBook);

            }
            catch (Exception arg)
            {
                errorMessages.Add(arg.Message);
            }
            if (errorMessages.Count > 0)
            {
                string errorMessage = string.Join(Environment.NewLine, errorMessages);
                AllAuthors = _bookManager.GetAllAuthors();
                TempData["errors"] = errorMessage;
                return Page();
            }
            TempData["Message"] = "Thank you for your contribution! Your book will be reviewed by our team.";
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostEBook()
        {
            var errorMessages = new List<string>();
            try
            {
                var ebook = new EBook(Book.Id, Book.Title, Book.Description, Book.Publisher, Book.Language,
               Book.PublicationDate, Book.Format, GetAuthorsByIds(SelectedAuthors), Filesize, DownloadLink);

                _bookManager.AddNewBook(ebook);

            }
            catch (Exception arg)
            {
                errorMessages.Add(arg.Message);
            }
            if (errorMessages.Count > 0)
            {
                string errorMessage = string.Join(Environment.NewLine, errorMessages);
                AllAuthors = _bookManager.GetAllAuthors();
                TempData["errors"] = errorMessage;
                return Page();
            }
            TempData["Message"] = "Thank you for your contribution! Your book will be reviewed by our team.";
            return RedirectToPage("/Index");
        }

        private List<Author> GetAuthorsByIds(List<int> authorIds)
        {
            var authors = _bookManager.GetAllAuthors();
            foreach (var author in authors)
            {
                if (authorIds.Contains(author.Id))
                {
                    return new List<Author> { author };
                }
            }

            return new List<Author>();
        }
    }
}
