using Classes;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Reflection;
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

                string query = "INSERT INTO Reviews (Comment, Rating, Date, Likes,Book_Id, User_Id) " +
                               "VALUES (@Comment, @Rating, @Date, @Likes,@BookId, @UserId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Comment", review.Comment);
                    command.Parameters.AddWithValue("@Rating", review.Rating);
                    command.Parameters.AddWithValue("@Date", review.Date);
                    command.Parameters.AddWithValue("@Likes", review.Likes);
                    command.Parameters.AddWithValue("@BookId", review.BookId);
                    command.Parameters.AddWithValue("@UserId", review.User.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool DeleteReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = "DELETE FROM Reviews WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", review.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public List<Review> GetAllReviewsByBook(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = "SELECT Reviews.Id, Reviews.Comment, Reviews.Rating, Reviews.Date, Reviews.Likes,Users.Id, Users.Name, Users.Username, Users.ProfilePicturePath FROM Reviews " +
                    "INNER JOIN Users ON Reviews.User_id = Users.Id " +
                    "WHERE Reviews.Book_id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", bookId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Review> result = new List<Review>();
                        while (reader.Read())
                        {
                            int reviewId = reader.GetInt32(0);
                            string comment = reader.GetString(1);
                            int rating = reader.GetInt32(2);
                            DateTime date = reader.GetDateTime(3);
                            int likes = reader.GetInt32(4);
                            User user = new User(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                            Review review = new Review(reviewId, comment, rating, date, likes, user, bookId);
                            result.Add(review);
                        }
                        return result;
                    }
                }
            }
        }

        public List<Review> GetAllReviewsByUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = "SELECT Reviews.Id, Reviews.Comment, Reviews.Rating, Reviews.Date, Reviews.Likes, Reviews.Book_id,Users.Id, Users.Name, Users.Username FROM Reviews " +
                    "INNER JOIN Users ON Reviews.User_id = Users.Id " +
                    "WHERE Users.Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Review> result = new List<Review>();
                        while (reader.Read())
                        {
                            int reviewId = reader.GetInt32(0);
                            string comment = reader.GetString(1);
                            int rating = reader.GetInt32(2);
                            DateTime date = reader.GetDateTime(3);
                            int likes = reader.GetInt32(4);
                            int bookId = reader.GetInt32(5);
                            User user = new User(reader.GetInt32(6), reader.GetString(7), reader.GetString(8));
                            Review review = new Review(reviewId, comment, rating, date, likes, user, bookId);
                            result.Add(review);
                        }
                        return result;
                    }
                }
            }
        }

        public Review? GetReview(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = "SELECT Reviews.Comment, Reviews.Rating, Reviews.Date, Reviews.Likes, Reviews.Book_id, Users.Id, Users.Name, Users.Username FROM Reviews " +
                    "INNER JOIN Users ON Reviews.User_id = Users.Id " +
                    "WHERE Reviews.Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", reviewId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string comment = reader.GetString(0);
                            int rating = reader.GetInt32(1);
                            DateTime date = reader.GetDateTime(2);
                            int likes = reader.GetInt32(3);
                            int bookId = reader.GetInt32(4);
                            User user = new User(reader.GetInt32(5), reader.GetString(6), reader.GetString(7));
                            Review review = new Review(reviewId, comment, rating, date, likes, user, bookId);
                            return review;
                        }

                    }
                }
                return null;
            }
        }

        public void LikeReview(int reviewId, int userId, string voteType)
        {
            using (SqlConnection con = new SqlConnection(DbConnectionString.Get()))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        string query = "";
                        if (voteType == "upVote")
                        {
                            query = "UPDATE Reviews SET Likes = Likes + 1 WHERE Id = @id";
                        }
                        else if (voteType == "downVote")
                        {
                            query = "UPDATE Reviews SET Likes = Likes - 1 WHERE Id = @id";
                        }
                        using (SqlCommand command = new SqlCommand(query, con, transaction))
                        {
                            command.Parameters.AddWithValue("id", reviewId);
                            command.ExecuteNonQuery();
                        }

                        string query2 = "INSERT INTO ReviewLikes (User_id, Review_id, Opinion) VALUES (@userId, @reviewId, @voteType)";
                        using (SqlCommand command2 = new SqlCommand(query2, con, transaction))
                        {
                            command2.Parameters.AddWithValue("userId", userId);
                            command2.Parameters.AddWithValue("reviewId", reviewId);
                            command2.Parameters.AddWithValue("voteType", voteType);
                            command2.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        transaction.Rollback();
                    }
                }
            }
        }


        public bool UpdateReview(Review review)
        {
            using (SqlConnection con = new SqlConnection(DbConnectionString.Get()))
            {
                con.Open();

                string query = "UPDATE Reviews SET Comment = @comment, Rating = @rating " +
                    "WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("comment", review.Comment);
                    command.Parameters.AddWithValue("rating", review.Rating);
                    command.Parameters.AddWithValue("id", review.Id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool HasUserReviewsOnBook(int userId, int bookId)
        {
            using (SqlConnection con = new SqlConnection(DbConnectionString.Get()))
            {
                con.Open();

                string query = "  SELECT Id FROM Reviews WHERE Book_id = @bookId AND User_id = @userId";
                using (var command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("userId", userId);
                    command.Parameters.AddWithValue("bookId", bookId);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public List<Review> GetReviewsPagination(int bookId, int currentPage, int pageSize)
        {
            int startIndex = (currentPage - 1) * pageSize + 1;
            int endIndex = startIndex + pageSize - 1;
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = " WITH RankedReviews AS(SELECT Reviews.Id, Reviews.Comment, Reviews.Rating, Reviews.Date, Reviews.Likes,Users.Id AS UserId, Users.Name, Users.Username,Users.ProfilePicturePath, ROW_NUMBER() OVER(ORDER BY Likes DESC) AS RowNumber FROM Reviews INNER JOIN Users ON Reviews.User_id = Users.Id WHERE Reviews.Book_id = @id) SELECT * FROM RankedReviews WHERE RowNumber BETWEEN @StartIndex AND @EndIndex";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", bookId);
                    command.Parameters.AddWithValue("StartIndex", startIndex);
                    command.Parameters.AddWithValue("EndIndex", endIndex);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Review> result = new List<Review>();
                        while (reader.Read())
                        {
                            int reviewId = reader.GetInt32(0);
                            string comment = reader.GetString(1);
                            int rating = reader.GetInt32(2);
                            DateTime date = reader.GetDateTime(3);
                            int likes = reader.GetInt32(4);
                            User user = new User(reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                            Review review = new Review(reviewId, comment, rating, date, likes, user, bookId);
                            result.Add(review);
                        }
                        return result;
                    }
                }
            }
        }

        public int GetReviewCountByBook(int bookId)
        {
            using (var connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "SELECT COUNT(Id) FROM Reviews WHERE Book_Id = @bookId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("bookId", bookId);
                    return (int)command.ExecuteScalar();
                }

            }
        }
    }
}
