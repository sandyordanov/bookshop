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
        Review? GetReview(int reviewId);
        List<Review> GetAllReviewsByBook(int bookId);
        List<Review> GetAllReviewsByUser(int userId);
        bool HasUserReviewsOnBook(int userId, int bookId);
        //pagination
        List<Review> GetReviewsPagination(int bookId, int startIndex, int pageSize);
        int GetReviewCountByBook(int bookId);
        //votes on reviews
        void LikeReview(int reviewId, int userId, string voteType);
        void ChangeVoteTypeOnAReview(int reviewId, int userId, string voteType);
    }
}
