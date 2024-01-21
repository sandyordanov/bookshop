using BLL.StrategyFilters;
using Classes;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BookManager
    {
        private IBookRepository _bookRepo;
        private IReviewRepository _reviewRepo;

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
            List<Review> reviews = _reviewRepo.GetAllReviewsByBook(book);
            StatisticsCalculator calculator = new StatisticsCalculator(reviews);
            return calculator.CalculateStatistics();
        }

        public Dictionary<Book, Statistics> SortBooksByRating(List<Book> books)
        {
            Dictionary<Book, Statistics> sortedList = new Dictionary<Book, Statistics>();
            foreach (var book in books)
            {
                sortedList.Add(book, GetBookStatistics(book));
            }
            sortedList = sortedList.OrderByDescending(x => x.Value.Average).ToDictionary(pair => pair.Key, pair => pair.Value);

            return sortedList;
        }

        public List<Book> SearchForBooks(string search, List<Book> books)
        {
            SearchEngine searchEngine = new SearchEngine();
            var result = searchEngine.SearchForBooks(search, books);
            return result;
        }
    }
}
