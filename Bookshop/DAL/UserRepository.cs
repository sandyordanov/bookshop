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
            string query = @"SELECT u.Name, u.Email, u.Username, u.Password, u.ProfilePicturePath FROM Users u
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

                            if (user == null)
                            {
                                user = new User(
                                     id,
                                     reader.GetString(0),
                                     reader.GetString(1),
                                     reader.GetString(2),
                                     reader.GetString(3),
                                     reader.GetString(4)
                                );

                            }

                        }
                        return user;
                    }
                }
            }
            return null;
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


                string query = @" SELECT u.Id, u.Name, u.Email FROM Users u";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var result = new List<User>();

                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string email = reader.GetString(2);

                            User user;


                            user = new User(userId, name, email);

                            result.Add(user);
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
        public void AddPowerUser(int userId)
        {
            using (SqlConnection con = new SqlConnection(DbConnectionString.Get()))
            {
                con.Open();
                string query = "INSERT INTO PowerUsers (User_id) VALUES (@id)";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("id", userId);
                    com.ExecuteNonQuery();
                }
            }
        }
        public bool CheckIfUserIsPowerUser(int userId)
        {
            using (SqlConnection con = new SqlConnection(DbConnectionString.Get()))
            {
                con.Open();
                string query = "SELECT * FROM PowerUsers WHERE User_id = @id";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("id", userId);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
    }
}
