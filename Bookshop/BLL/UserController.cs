using BCrypt.Net;
using Classes;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserController
    {
        User? logged;
        IUserRepository _userRepository;
        public UserController()
        {
            _userRepository = new UserRepository();
        }
        public UserController(int userId)
        {
            logged = GetUserById(userId);
            _userRepository = new UserRepository();

        }
        public User GetUserById(int id)
        {
            User user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("User return was null - user not found");
            }
            else { return user; }
        }
        public bool RegisterUser(User user)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt, true);
            user.Salt = salt;
            return _userRepository.RegisterUserProfile(user);
        }
        public int TryToLogUserIn(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password, true))
                {
                    return user.Id;
                }
                return 0;
            }
        }
    }
}
