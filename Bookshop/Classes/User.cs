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
        public int Id{ get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Enter a valid email adress.")]
        public string Email { get; set; }
    }
}
