using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classes
{
    public class User
    {
        private int _id;
        private string _name;
        private string _email;
        private string? _username;
        private string? _password;
        private string _picturePath;

        public int Id { get => _id; }
        
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20 symbols")]
        [RegularExpression(@"^[A-Za-z\-]+$", ErrorMessage = "Name must contain only letters or hyphens")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty or whitespace.");
                }
                _name = value;
            }
        }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20 symbols")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email cannot be empty or whitespace.");
                }
                _email = value;
            }
        }
        [MinLength(3,ErrorMessage ="Minimun length is 3 symbols")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20 symbols")]
        [Required(ErrorMessage = "Username is required.")]
        public string? Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be empty or whitespace.");
                }
                _username = value;
            }
        }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
         ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string? Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password cannot be empty or whitespace.");
                }
                _password = value;
            }
        }


        public List<Review>? Reviews { get; set; }
        public string PicturePath { get => _picturePath; set => _picturePath = value; }
        public Dictionary<int, string> LikedReviews { get; set; }

        public User()
        {

        }
        public User(int id, string name, string username)
        {
            _id = id;
            Name = name;
            Username = username;
        }
        public User(int id, string name, string username, string pp)
        {
            _id = id;
            Name = name;
            Username = username;
            PicturePath = pp;
        }
        public User(int id, string name, string email, List<Review> reviews)
        {
            _id = id;
            Name = name;
            Email = email;
            Reviews = reviews;
        }
        public User(int id, string name, string email, string username, string password, string picturePath)
        {
            _id = id;
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            PicturePath = picturePath;

        }
    }
}
