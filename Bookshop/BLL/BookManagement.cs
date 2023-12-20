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
        public BookManagement(IBookRepository bookRep, IReviewRepository revRep)
        {
            _bookRepo = bookRep;
            _reviewRepo = revRep;
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
        public bool UpdateBook(Book book)
        {
            return _bookRepo.UpdateBook(book);
        }
        public List<Book> GetAllBooks()
        {
            return books;
        }
        public void RefreshCollection()
        {
            books = _bookRepo.GetAllBooks();
        }
        public Book? GetBookInfo(int id)
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
        public Book GetBookAndReviews(int id)
        {
            Book? book = books.FirstOrDefault(book => book.Id == id);
            if (book == null) { throw new Exception("Book with this ID not found"); }
            List<Review> reviews = _reviewRepo.GetAllReviewsByBook(id);
            foreach (Review review in reviews)
            {
                book.AddReview(review);
            }
            return book;
        }
        public List<Author> GetAllAuthors()
        {
            return _bookRepo.GetAllAuthors();
        }

        public List<Format> GetAllFormats()
        {
            return _bookRepo.GetAllFormats();
        }

        public void DeleteBook(Book book)
        {
            books.Remove(book);
            _bookRepo.DeleteBook(book);
        }
        public List<Review> GetAllReviewsByBook(int bookId)
        {
            return _reviewRepo.GetAllReviewsByBook(bookId);
        }
        public List<Review> GetAllReviewsByUser(int userId)
        {
            return _reviewRepo.GetAllReviewsByUser(userId);
        }
        public bool AddReview(Review review)
        {
            return _reviewRepo.AddReview(review);
        }
        public bool UpdateReview(Review review)
        {
            return _reviewRepo.UpdateReview(review);
        }
        public bool DeleteReview(Review review)
        {
            return _reviewRepo.DeleteReview(review);
        }
        public void LikeReview(Review review)
        {
            _reviewRepo.LikeReview(review.Id);
            books.First(book => book.Id == review.BookId).LikeReview(review.Id);
        }
        public Review GetReview(int reviewId)
        {
            if (_reviewRepo.GetReview(reviewId) != null)
            {
                return _reviewRepo.GetReview(reviewId);
            }
            throw new Exception("review not fount");
        }
    }
}
