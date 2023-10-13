using Classes;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserServices
    {
        IUserRepository userRepository;
        public UserServices()
        {
             userRepository = new UserRepository();
        }
        public User GetUser(string username)
        {
            return userRepository.GetUserByUsername(username);
        }
    }
}
