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
        bool RegisterUserProfile(User user);
        bool UpdateUserProfile(User user);
        bool DeleteUserProfile(User user);

    }
}
