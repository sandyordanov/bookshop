using Classes;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Fakes
{
    public class FakeReviewDbMediator : IReviewRepository
    {
        private List<Review> _reviews = new List<Review>();
        private List<User> _users = new List<User>();
        private List<Book> _books = new List<Book>();
        public FakeReviewDbMediator()
        {
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            _users.Add(user1);
            _users.Add(user2);
            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "0293829311", "1233124212");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(2, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");
            _books.Add(book1);
            _books.Add(book2);
            var review1 = new Review(1, "Great book!", 5,DateTime.Now,4, user1, book1);
            var review2 = new Review(2, "Great book!", 2, DateTime.Now, 2, user2, book2);
            _reviews.Add(review1);
            _reviews.Add(review2);
        }
        public bool AddReview(Review review)
        {
           _reviews.Add(review);
            return true;
        }

        public void ChangeVoteTypeOnAReview(int reviewId, int userId, string voteType)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReview(Review review)
        {
           _reviews.Remove(review);
            return true;
        }

        public List<Review> GetAllReviews()
        {
            return _reviews;
        }

        public List<Review> GetAllReviewsByBook(Book book)
        {
            return _reviews.Where(r => r.Book.Id == book.Id).ToList();
        }

        public List<Review> GetAllReviewsByUser(User user)
        {
           return _reviews.Where(r => r.User.Id == user.Id).ToList();
        }

        public Review? GetReview(int reviewId)
        {
           return _reviews.FirstOrDefault(r => r.Id == reviewId);
        }

        public int GetReviewCountByBook(int bookId)
        {
            return _reviews.Where(r => r.Book.Id == bookId).Count();
        }

        public List<Review> GetReviewsPagination(Book book, int startIndex, int pageSize)
        {
            return _reviews.Where(r => r.Book.Id == book.Id).Skip(startIndex).Take(pageSize).ToList();
        }

        public bool HasUserReviewsOnBook(int userId, int bookId)
        {
            return _reviews.Any(r => r.User.Id == userId && r.Book.Id == bookId);
        }

        public void LikeReview(int reviewId, int userId, string voteType)
        {
            
        }

        public bool UpdateReview(Review review)
        {
            return true;
        }
    }
}
