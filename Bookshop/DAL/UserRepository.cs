using Classes;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        public bool DeleteUserProfile(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            string query = "SELECT Name, Email, Username, Password, Salt FROM Users WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User(
                                 id,
                                 reader.GetString(0),
                                 reader.GetString(1),
                                 reader.GetString(2),
                                 reader.GetString(3)
                                 );
                            user.Salt = reader.GetString(4);
                            return user;
                        }
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
                                reader.GetString(4)
                                );
                            user.Salt = reader.GetString(5);
                            conn.Close();
                            return user;
                        }

                    }
                }
                conn.Close();
                return null;
            }
        }

        public User GetUserInfo(int id)
        {
            string query = "SELECT Name, Email FROM Users WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User(
                                 id,
                                 reader.GetString(0),
                                 reader.GetString(1)
                                 );
                            conn.Close();
                            return user;
                        }
                    }
                }
                conn.Close();
            }
            return null;
        }
        public bool RegisterUserProfile(User user)
        {
            using SqlConnection conn = new SqlConnection(DbConnectionString.Get());
            using var command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "INSERT INTO USERS (Name, Email, Username, Password, Salt) VALUES ( @name, @email, @username, @password, @salt )";
            command.Parameters.AddWithValue("name", user.Name);
            command.Parameters.AddWithValue("email", user.Email);
            command.Parameters.AddWithValue("username", user.Username);
            command.Parameters.AddWithValue("password", user.Password);
            command.Parameters.AddWithValue("salt", user.Salt);
            var result = command.ExecuteNonQuery();
            conn.Close();
            return result == 1;
        }

        public bool UpdateUserProfile(User user)
        {
            throw new NotImplementedException();
        }
    }
}
