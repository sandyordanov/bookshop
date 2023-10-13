using Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByUsername(string username)
        {
            User user = new User();
            string query = "SELECT * FROM Users WHERE Username = @username";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                Email = reader.GetString(2),
                                Username = reader.GetString(3),
                                Password = reader.GetString(4),
                            };
                        }

                    }
                }
                return user;
            }
        }
    }
}
