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
        User GetUserInfo(int id);
        User GetUserById(int id);
        bool RegisterUserProfile(User user);
        bool UpdateUserProfile(User user);
        bool DeleteUserProfile(User user);

    }
}
