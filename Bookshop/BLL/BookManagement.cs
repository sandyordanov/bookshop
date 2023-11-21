using Classes;
using DAL;
using DAL.Interfaces;
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
        private IReviewRepository _reviewRepo;
        private List<Book> books;
        public BookManagement(IBookRepository som)
        {
            _bookRepo =som;
            _reviewRepo = new ReviewRepository();
            books = _bookRepo.GetAllBooks();
        }
        public IEnumerable<Book> FilterByStrategies(List<IBookFilterStrategy> strategyCollection) 
        {
            BookFilterContext filterMachine = new BookFilterContext(strategyCollection);
            return filterMachine.FilterBooks(books);
        }
        public bool AddNewBook(Book book)
        {
            books.Add(book);
           return _bookRepo.AddBook(book);
        }
        public List<Book> GetAllBooks()
        {
            return books;
        }
        public void RefreshCollection()
        {
            books = _bookRepo.GetAllBooks();
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
        public List<Author> GetAllAuthors()
        {
            return _bookRepo.GetAllAuthors();
        }

        public List<Format> GetAllFormats()
        {
            return _bookRepo.GetAllFormats();
        }

        public void DeleteBook(Book? book)
        {
            _bookRepo.DeleteBook(book);
        }
        public List<Review> GetAllReviews(int bookId)
        {
           return _reviewRepo.GetAllReviews(bookId);
        }
        public bool AddReview(Review review)
        {
            return _reviewRepo.AddReview(review);
        }
    }
}
