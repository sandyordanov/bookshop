using Classes;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
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
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = "INSERT INTO Reviews (Comment, Rating, Date, Likes, UserId) " +
                               "VALUES (@Comment, @Rating, @Date, @Likes, @UserId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Comment", review.Comment);
                    command.Parameters.AddWithValue("@Rating", review.Rating);
                    command.Parameters.AddWithValue("@Date", review.Date);
                    command.Parameters.AddWithValue("@Likes", review.Likes);
                    command.Parameters.AddWithValue("@UserId", review.User.Id); 

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
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
