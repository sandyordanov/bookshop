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
        void DeleteReview(Review review);

        Review GetReview(int reviewId);
        List<Review> GetAllReviews(int bookId);

    }
}
