using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class BookModel : PageModel
    {
        [BindProperty]
        public Book Book { get; set; }
        private BookManagement bookManager;
        public BookModel(IBookRepository repos)
        {
            bookManager = new BookManagement(repos);
        }
        public void OnGet(int id)
        {
            Book = bookManager.GetBookInfo(Convert.ToInt32(HttpContext.Session.GetString("id")));
        }
    }
}
