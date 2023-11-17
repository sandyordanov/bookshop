using Classes;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BookManagement
    {
        private IBookRepository _bookRepo;
        private List<Book> books;
        public BookManagement()
        {
            _bookRepo = new BookRepository();
            books = _bookRepo.GetAllBooks();
        }
        public void AddNewPaperBook(PaperBook book)
        {
            books.Add(book);
            _bookRepo.AddBook(book);
        }
        public List<Book> GetAllBooks()
        {
            return books;
        }
        public Book GetBookInfo(int id)
        {
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }
            return null;
        }
    }
}
