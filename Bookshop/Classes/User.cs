using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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

        public int Id { get => _id; }
        [Required]
        public string Name { get => _name; set => _name = value; }
        [EmailAddress(ErrorMessage = "Enter a valid email adress.")]
        public string Email { get => _email; set => _email = value; }
        [Required]
        public string? Username { get => _username; set => _username = value; }
        public string? Password { get => _password; set => _password = value; }
        public string? Salt { get; set; }
        public User()
        {
            
        }
        public User(int id, string name, string email)
        {
            _id = id;
            Name = name;
            Email = email;
        }
        public User(int id, string name, string email, string username, string password)
        {
            _id = id;
            Name = name;
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
