using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StatisticsCalculator
    {
        private List<Review> reviews;
        private Statistics statistics;

        public StatisticsCalculator(List<Review> _reviews)
        {
            reviews = _reviews;
            statistics = new Statistics();
        }

        public Statistics CalculateStatistics()
        {
            SeparateReviews(reviews);
 
            statistics.ReviewCount = reviews.Count;
            statistics.Sum = reviews.Select(rev => rev.Rating).Sum();
            
            statistics.GeneratePercentages();
            return statistics;
        }
        private void SeparateReviews(List<Review> revs)
        {
            foreach (var review in revs)
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
        }
    }
}
