using Classes;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReviewManager
    {
        private readonly IReviewRepository _reviewRepo;
        public ReviewManager(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public bool AddReview(Review review)
        {
            return _reviewRepo.AddReview(review);
        }
        public Review GetReview(int reviewId)
        {
            var review = _reviewRepo.GetReview(reviewId);

            if (review != null)
            {
                return review;
            }

            throw new Exception("review not found");
        }
        public List<Review> GetAllReviews()
        {
            return _reviewRepo.GetAllReviews();
        }
        public List<Review> GetAllReviewsByBook(Book book)
        {
            return _reviewRepo.GetAllReviewsByBook(book);
        }
        public List<Review> GetAllReviewsByUser(User user)
        {
            return _reviewRepo.GetAllReviewsByUser(user);
        }
        public Review GetReviewOnBookByUser(User user, Book book)
        {
            var reviewsByUser = _reviewRepo.GetAllReviewsByUser(user);
            foreach (var review in reviewsByUser)
            {
                if (review.Book.Id == book.Id)
                {
                    return review;
                }
            }
            return null;
        }
        public bool UpdateReview(Review review)
        {
            return _reviewRepo.UpdateReview(review);
        }
        public bool DeleteReview(Review review)
        {
            return _reviewRepo.DeleteReview(review);
        }

        //votes on reviews logic
        public void VoteOnReview(Review review, User user, string voteType)
        {
            if (user.LikedReviews.ContainsKey(review.Id) || user.DislikedReviews.ContainsKey(review.Id))
            {
                ChangeVoteTypeOnAReview(review.Id, user.Id, voteType);
                if (voteType == "downVote")
                {
                    user.LikedReviews.Remove(review.Id);
                    user.DislikedReviews.Add(review.Id, voteType);
                }
                else if (voteType == "upVote")
                {
                    user.DislikedReviews.Remove(review.Id);
                    user.LikedReviews.Add(review.Id, voteType);
                }
            }
            else
            {
                _reviewRepo.LikeReview(review.Id, user.Id, voteType);
            }
        }
        public void ChangeVoteTypeOnAReview(int reviewId, int userId, string voteType)
        {
            _reviewRepo.ChangeVoteTypeOnAReview(reviewId, userId, voteType);
        }
        //interface review checks
        public bool UserHasReviewsOnBook(int userId, int bookId)
        {
            return _reviewRepo.HasUserReviewsOnBook(userId, bookId);
        }

        //pagination logic
        public List<Review> GetReviewsPagination(Book book, int currentPage, int pagesize)
        {
            return _reviewRepo.GetReviewsPagination(book, currentPage, pagesize);
        }
        public int GetTotalReviewCountForBook(int bookId)
        {
            return _reviewRepo.GetReviewCountByBook(bookId);
        }
    }
}
