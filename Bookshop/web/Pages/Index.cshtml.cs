using BLL;
using Classes;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace web.Pages
{
    public class IndexModel : PageModel
    {
        private BookManager bookManager;

        public Dictionary<Book, Statistics> TopBooks { get; set; }


        public IndexModel(IBookRepository bookRepo, IReviewRepository reviewRepo)
        {
            bookManager = new BookManager(bookRepo, reviewRepo);
        }

        public void OnGet()
        {
            TopBooks = bookManager.SortBooksByRating(bookManager.GetAllBooks()).Take(12).ToDictionary(pair => pair.Key, pair => pair.Value); ;
        }
        public IActionResult OnPost(int? id)
        {
            return RedirectToPage("/Book", id);
        }

    }
}