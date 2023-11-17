using BLL;
using Classes;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web.Pages
{
    public class BookModel : PageModel
    {
        public Book Book { get; set; }
        private BookManagement bookManager;
        public BookModel()
        {
            bookManager = new BookManagement();
        }
        public void OnGet(int id)
        {
            Book = bookManager.GetBookInfo(Convert.ToInt32(HttpContext.Session.GetString("id")));
        }
    }
}
