using BLL;
using Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;

namespace web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private BookManagement bookManager;


        [BindProperty(SupportsGet = true)]
        public List<PaperBook> books { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            bookManager = new BookManagement();
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