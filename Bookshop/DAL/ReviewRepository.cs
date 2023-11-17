using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReviewRepository : IReviewRepository
    {
        public bool AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Review review)
        {

        }
        public List<Review> GetAllReviews(int bookId)
        {
            return null;
        }
        public Review GetReview(int reviewId)
        {
            throw new NotImplementedException();
        }
    }
}
