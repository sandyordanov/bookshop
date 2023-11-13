using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ReviewManager
    {
        private List<Review> reviews;

        public ReviewManager()
        {
            reviews = new List<Review>();
        }
        public void AddReview(Review review)
        {
            reviews.Add(review);
        }
        public void RemoveReview(Review review)
        {
            reviews.Remove(review);
        }
        public List<Review> GetReviews()
        {
            return reviews;
        }
    }
}
