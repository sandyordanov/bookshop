using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IReviewRepository
    {
        bool AddReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        //the gets
        Review? GetReview(int reviewId);
        List<Review> GetAllReviewsByBook(Book book);
        List<Review> GetAllReviewsByUser(User user);
        //user review checks
        bool HasUserReviewsOnBook(int userId, int bookId);
        //pagination
        List<Review> GetReviewsPagination(Book book, int startIndex, int pageSize);
        int GetReviewCountByBook(int bookId);
        //votes on reviews
        void LikeReview(int reviewId, int userId, string voteType);
        void ChangeVoteTypeOnAReview(int reviewId, int userId, string voteType);
    }
}
