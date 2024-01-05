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
            foreach (Book book in books)
            {
               var reviews = GetAllReviewsByBook(book.Id);
                foreach (Review review in reviews)
                {
                    book.AddReview(review);
                }
            }
        }
        public void RefreshCollection()
        {
            books = _bookRepo.GetAllBooks();
        }

        //Book CRUD
        public bool AddNewBook(Book book)
        {
            books.Add(book);
            return _bookRepo.AddBook(book);
        }
        public List<Book> GetAllBooks()
        {
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
        public bool UpdateBook(Book book)
        {
            return _bookRepo.UpdateBook(book);
        }
        public void DeleteBook(Book book)
        {
            books.Remove(book);
            _bookRepo.DeleteBook(book);
        }

        // reviews CRUD
        public bool AddReview(Review review)
        {
            return _reviewRepo.AddReview(review);
        }
        public Review GetReview(int reviewId)
        {
            if (_reviewRepo.GetReview(reviewId) != null)
            {
                return _reviewRepo.GetReview(reviewId);
            }
            throw new Exception("review not found");
        }
        public List<Review> GetAllReviewsByBook(int bookId)
        {
            return _reviewRepo.GetAllReviewsByBook(bookId);
        }
        public List<Review> GetAllReviewsByUser(int userId)
        {
            return _reviewRepo.GetAllReviewsByUser(userId);
        }
        public bool UpdateReview(Review review)
        {
            return _reviewRepo.UpdateReview(review);
        }
        public bool DeleteReview(Review review)
        {
            return _reviewRepo.DeleteReview(review);
        }

        //votes on reviews logic
        public void LikeReview(Review review, User user, string voteType)
        {
            if (user.LikedReviews.ContainsKey(review.Id) || user.DislikedReviews.ContainsKey(review.Id))
            {
                ChangeVoteTypeOnAReview(review.Id, user.Id, voteType);
                if (voteType == "downVote")
                {
                    user.LikedReviews.Remove(review.Id);
                    user.DislikedReviews.Add(review.Id,voteType);
                }
                else if(voteType == "upVote")
                {
                    user.DislikedReviews.Remove(review.Id);
                    user.LikedReviews.Add(review.Id, voteType);
                }
            }
            else
            {
                _reviewRepo.LikeReview(review.Id, user.Id, voteType);
            }
        }
        public void ChangeVoteTypeOnAReview(int reviewId, int userId, string voteType)
        {
            _reviewRepo.ChangeVoteTypeOnAReview(reviewId,userId,voteType);
        }

        //interface review checks
        public bool UserHasReviewsOnBook(int userId, int bookId)
        {
            return _reviewRepo.HasUserReviewsOnBook(userId, bookId);
        }

        //pagination logic
        public List<Review> GetReviewsPagination(int bookId, int currentPage, int pagesize)
        {
            return _reviewRepo.GetReviewsPagination(bookId, currentPage, pagesize);
        }
        public int GetTotalReviewCountForBook(int bookId)
        {
            return _reviewRepo.GetReviewCountByBook( bookId);
        }

        // filter logic
        public IEnumerable<Book> FilterByStrategies(List<IBookFilterStrategy> strategyCollection)
        {
            BookFilterContext filterMachine = new BookFilterContext(strategyCollection);
            return filterMachine.FilterBooks(books);
        }
    }
}
