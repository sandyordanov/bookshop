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
        private Statistics statistics;
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
        public Statistics GetStatistics()
        {
            statistics = new Statistics();
            statistics.ReviewCount = _reviews.Count;
            statistics.Sum = _reviews.Select(rev => rev.Rating).Sum();
            foreach (var review in _reviews)
            {
                if (review.Rating == 1)
                {
                    statistics.oneStarReviewsCount++;
                }
                else if (review.Rating == 2)
                {
                    statistics.twoStarReviewsCount++;
                }
                else if (review.Rating == 3)
                {
                    statistics.threeStarReviewsCount++;
                }
                else if (review.Rating == 4)
                {
                    statistics.fourStarReviewsCount++;
                }
                else if (review.Rating == 5)
                {
                    statistics.fiveStarReviewsCount++;
                }
            }
            statistics.GeneratePercentages();
            return statistics;

        }

        public Review GetReviewByUser(int userId)
        {
            var result = _reviews.Find(x => x.User.Id == userId);
            if (result != null)
            {
                _reviews.Remove(result);
                return result;
            }
            return null;
        }
    }
}
