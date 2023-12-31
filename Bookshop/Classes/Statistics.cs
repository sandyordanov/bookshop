using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Statistics
    {
        public int oneStarReviewsCount { get; set; }
        public int twoStarReviewsCount { get; set; }
        public int threeStarReviewsCount { get; set; }
        public int fourStarReviewsCount { get; set; }
        public int fiveStarReviewsCount { get; set; }
        public double oneStarReviewsPercentage { get; set; }
        public double twoStarReviewsPercentage { get; set; }
        public double threeStarReviewsPercentage { get; set; }
        public double fourStarReviewsPercentage { get; set; }
        public double fiveStarReviewsPercentage { get; set; }
        public double  Average { get; set; }
        public double ReviewCount { get; set; }
        public double Sum { get; set; }
        public void GeneratePercentages()
        {
            oneStarReviewsPercentage =Math.Round( oneStarReviewsCount *100/ReviewCount);
            twoStarReviewsPercentage = Math.Round(twoStarReviewsCount * 100 /ReviewCount);
            threeStarReviewsPercentage = Math.Round(threeStarReviewsCount * 100/ ReviewCount);
            fourStarReviewsPercentage = Math.Round(fourStarReviewsCount * 100 / ReviewCount);
            fiveStarReviewsPercentage = Math.Round(fiveStarReviewsCount * 100 / ReviewCount);
            Average =Math.Round( Sum / ReviewCount,2);
        }
    }
}
