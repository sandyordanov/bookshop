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
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepo)
        {
            _userRepository = userRepo;
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
        public User GetUserLikes(User user)
        {
            user.LikedReviews = GetLikedReviews(user.Id);
            user.DislikedReviews = GetDislikedReviews(user.Id);
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
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public void InsertProfilePicture(int userId, string picturePath)
        {
            _userRepository.InsertProfilePicture(userId, picturePath);
        }
        public bool UpdateUserProfile(User user)
        {
            if (!user.Password.Contains("$2a$11$"))
            {
                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt, true);
            }
            return _userRepository.UpdateUserProfile(user);
        }
        public Dictionary<int, string> GetLikedReviews(int userId)
        {
            return _userRepository.GetLikedReviews(userId);
        }
        private Dictionary<int, string> GetDislikedReviews(int userId)
        {
            return _userRepository.GetDislikedReviews(userId);
        }
        public void AddPowerUser(int userId)
        {
            _userRepository.AddPowerUser(userId);
        }
        public bool CheckIfUserIsPowerUser(int userId)
        {
            return _userRepository.CheckIfUserIsPowerUser(userId);
        }
        public bool DeleteUserProfile(User user)
        {
            return _userRepository.DeleteUserProfile(user);
        }
        
    }
}
