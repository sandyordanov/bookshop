using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Classes
{
    public class ReviewManager
    {
        private List<Review> _reviews;
        public ReviewManager()
        {
            _reviews = new List<Review>();
        }
        public void AddReview(Review review)
        {
            _reviews.Add(review);
        }
        public void AddAllReviews(List<Review> reviews)
        {

        }
        public void LikeReview(int reviewId)
        {
          var likedReview = _reviews.First(x => x.Id == reviewId);
            likedReview.Likes++;
            
        }
        public void RemoveReview(Review review)
        {
            _reviews.Remove(review);
        }
        public List<Review> GetReviews()
        {
            return _reviews;
        }
    }
}
