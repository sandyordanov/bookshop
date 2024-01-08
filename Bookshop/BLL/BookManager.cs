using BLL.StrategyFilters;
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
    public class BookManager
    {
        private IBookRepository _bookRepo;
        private IReviewRepository _reviewRepo;

        //private List<Book> books;

        public BookManager(IBookRepository bookRep, IReviewRepository revRep)
        {
            _bookRepo = bookRep;
            _reviewRepo = revRep;
        }

        //Book CRUD
        public bool AddNewBook(Book book)
        {
            return _bookRepo.AddBook(book);
        }
        public List<Book> GetAllBooks()
        {
            return _bookRepo.GetAllBooks();
        }
        public List<Book> GetAllBooksWithReviews()
        {
            var books = _bookRepo.GetAllBooks();
            foreach (var book in books)
            {
                book.AddReviews(_reviewRepo.GetAllReviewsByBook(book));
            }
            return books;
        }
        public List<Author> GetAllAuthors()
        {
            return _bookRepo.GetAllAuthors();
        }
        public List<Format> GetAllFormats()
        {
            return _bookRepo.GetAllFormats();
        }
        public Book GetBook(int bookId)
        {
            var book = _bookRepo.GetBook(bookId);
            book.AddReviews(_reviewRepo.GetAllReviewsByBook(book));
            return book;
        }
        public bool UpdateBook(Book book)
        {
            return _bookRepo.UpdateBook(book);
        }
        public void DeleteBook(Book book)
        {
            _bookRepo.DeleteBook(book);
        }

        // filter logic
        public IEnumerable<Book> FilterByStrategies(List<IBookFilterStrategy> strategyCollection)
        {
            var books = _bookRepo.GetAllBooks();
            BookFilterContext filterMachine = new BookFilterContext(strategyCollection);
            return filterMachine.FilterBooks(books);
        }
        //statistics
        public Statistics GetBookStatistics(Book book)
        {
            StatisticsCalculator calculator = new StatisticsCalculator(book.GetReviews());
            return calculator.CalculateStatistics();
        }
    }
}
