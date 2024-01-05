using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        List<User> GetAllUsers();
        User GetUserInfo(int id);
        User GetUserById(int id);
        bool RegisterUserProfile(User user);
        bool UpdateUserProfile(User user);
        bool DeleteUserProfile(User user);

        void InsertProfilePicture(int userId, string picturePath);
        Dictionary<int, string> GetLikedReviews(int userId);
        Dictionary<int, string> GetDislikedReviews(int userId);
    }
}
