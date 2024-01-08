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
        private Dictionary<int, Review> reviews;
        public FakeReviewDbMediator()
        {
            reviews = new Dictionary<int, Review>();
        }
        public bool AddReview(Review review)
        {
            reviews.Add(review.Id, review);
            return true;
        }

        public void ChangeVoteTypeOnAReview(int reviewId, int userId, string voteType)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReview(Review review)
        {
           reviews.Remove(review.Id);
            return true;
        }

        public List<Review> GetAllReviewsByBook(Book book)
        {
            List<Review> result = new List<Review>();
            foreach (var item in reviews)
            {
                if (item.Value.Book.Id == book.Id)
                {
                    result.Add(item.Value);
                }
            }
            return result;
        }

        public List<Review> GetAllReviewsByUser(User user)
        {
           return reviews.Values.Where(r => r.User.Id == user.Id).ToList();
        }

        public Review? GetReview(int reviewId)
        {
           return reviews[reviewId];
        }

        public int GetReviewCountByBook(int bookId)
        {
            return reviews.Values.Where(r => r.Book.Id == bookId).Count();
        }

        public List<Review> GetReviewsPagination(Book book, int startIndex, int pageSize)
        {
            return reviews.Values.Where(r => r.Book.Id == book.Id).Skip(startIndex).Take(pageSize).ToList();
        }

        public bool HasUserReviewsOnBook(int userId, int bookId)
        {
            return reviews.Values.Where(r => r.User.Id == userId && r.Book.Id == bookId).Count() > 0;
        }

        public void LikeReview(int reviewId, int userId, string voteType)
        {
            foreach (var item in reviews)
            {
                if (item.Value.Id == reviewId)
                {
                    item.Value.Likes++;
                }
            }
        }

        public bool UpdateReview(Review review)
        {
            return true;
        }
    }
}
