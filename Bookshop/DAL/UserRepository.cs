using Classes;
using DAL.DbConnection;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        public bool DeleteUserProfile(User user)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = "DELETE FROM Users WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", user.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public User GetUserById(int id)
        {
            
            string query = @"
        SELECT u.Name, u.Email, u.Username, u.Password, u.ProfilePicturePath, 
               r.Id, r.Comment, r.Rating, r.Date, r.Likes, r.Book_id 
        FROM Users u
        LEFT JOIN Reviews r ON u.Id = r.User_id
        WHERE u.Id = @id";

            using (SqlConnection conn = new SqlConnection(DbConnectionString.Get()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        User user = null;
                        while (reader.Read())
                        {
                            // If the user object hasn't been created yet, create it
                            if (user == null)
                            {
                                user = new User(
                                     id,
                                     reader.GetString(0), // Name
                                     reader.GetString(1), // Email
                                     reader.GetString(2), // Username
                                     reader.GetString(3), // Password
                                     reader.GetString(4)  // ProfilePicturePath
                                );
                                user.Reviews = new List<Review>(); // Initialize the Reviews list
                            }

                            // Check if the Review fields are not DBNull, which means this user has reviews
                            if (!reader.IsDBNull(5))
                            {
                                var review = new Review(
                                    reader.GetInt32(5),    // Review Id
                                    reader.GetString(6),   // Comment
                                    reader.GetInt32(7),    // Rating
                                    reader.GetDateTime(8), // Date
                                    reader.GetInt32(9), // Date
                                    user,
                                    null
                                );
                                user.Reviews.Add(review);
                            }
                        }
                        return user;
                    }
                }
            }
            return null; // If user not found or no reviews
        }

        public User GetUserByUsername(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = @username";
            using (SqlConnection conn = new SqlConnection(DbConnectionString.Get()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5)
                                );
                            conn.Close();
                            return user;
                        }

                    }
                }
                conn.Close();
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                
                string query = @" SELECT u.Id, u.Name, u.Email, r.Id, r.Comment, r.Rating, r.Date, r.Likes, r.Book_id FROM Users u LEFT JOIN Reviews r ON u.Id = r.User_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var result = new List<User>();
                        var reviewsByUser = new Dictionary<int, List<Review>>();

                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string email = reader.GetString(2);

                            User user;
                            if (!reviewsByUser.ContainsKey(userId))
                            {
                                user = new User(userId, name, email);
                                user.Reviews = new List<Review>();
                                reviewsByUser[userId] = user.Reviews;
                                result.Add(user);
                            }
                            else
                            {
                                user = result.First(u => u.Id == userId);
                            }

                            if (!reader.IsDBNull(3)) // This means there are review fields
                            {
                                var review = new Review(
                                    reader.GetInt32(3),    // Review Id
                                    reader.GetString(4),   // Comment
                                    reader.GetInt32(5),    // Rating
                                    reader.GetDateTime(6), // Date
                                    reader.GetInt32(7),    // Likes
                                    user,null 
                                );
                                reviewsByUser[userId].Add(review);
                            }
                        }
                        return result;
                    }
                }
            }
        }


        public bool RegisterUserProfile(User user)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(DbConnectionString.Get());
                using var command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "INSERT INTO USERS (Name, Email, Username, Password, ProfilePicturePath) VALUES ( @name, @email, @username, @password, @ppp)";
                command.Parameters.AddWithValue("name", user.Name);
                command.Parameters.AddWithValue("email", user.Email);
                command.Parameters.AddWithValue("username", user.Username);
                command.Parameters.AddWithValue("password", user.Password);
                command.Parameters.AddWithValue("ppp", "noPic.png");
                var result = command.ExecuteNonQuery();
                conn.Close();

                return result == 1;
            }
            catch (SqlException)
            {
                return false;
            }

        }

        public bool UpdateUserProfile(User user)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string query = "Update Users Set Name = @name, Email = @email, Password = @password WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("password", user.Password);
                    command.Parameters.AddWithValue("id", user.Id);
                    int result = command.ExecuteNonQuery();
                    return result == 1;
                }
            }
        }

        public void InsertProfilePicture(int userId, string picturePath)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "UPDATE Users SET ProfilePicturePath = @picturePath WHERE Id = @id";
                using (var com = new SqlCommand(query, connection))
                {
                    com.Parameters.AddWithValue("id", userId);
                    com.Parameters.AddWithValue("picturePath", picturePath);
                    com.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<int, string> GetLikedReviews(int userId)
        {
            using (var sqlConnection = new SqlConnection(DbConnectionString.Get()))
            {
                sqlConnection.Open();
                string query = "SELECT Review_id, Opinion FROM ReviewLikes WHERE User_id = @id AND Opinion = 'upVote'";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("id", userId);
                    Dictionary<int, string> result = new Dictionary<int, string>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int reviewId = reader.GetInt32(0);
                            string opinion = reader.GetString(1);
                            result.Add(reviewId, opinion);
                        }
                        return result;
                    }
                }
            }
        }
        public Dictionary<int, string> GetDislikedReviews(int userId)
        {
            using (var sqlConnection = new SqlConnection(DbConnectionString.Get()))
            {
                sqlConnection.Open();
                string query = "SELECT Review_id, Opinion FROM ReviewLikes WHERE User_id = @id AND Opinion = 'downVote'";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("id", userId);
                    Dictionary<int, string> result = new Dictionary<int, string>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int reviewId = reader.GetInt32(0);
                            string opinion = reader.GetString(1);
                            result.Add(reviewId, opinion);
                        }
                        return result;
                    }
                }
            }
        }
    }
}
