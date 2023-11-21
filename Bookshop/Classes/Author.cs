using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Author
    {
        private int _id;
        private string _fullName;
        private DateTime _birthdate;
        private string _description;
        private string _website;
        private string _twitter;
        private List<string>? _genres;

        public int Id { get { return _id; } }
        public string FullName { get { return _fullName; } set { _fullName = value; } }
        public DateTime Birthdate { get => _birthdate; set => _birthdate = value; }
        public string Description { get { return _description; } set { _description = value; } }
        public string Website { get { return _website; } set { _website = value; } }
        public string Twitter { get { return _twitter; } set { _twitter = value; } }
        public List<string>? Genres { get { return _genres; } set { _genres = value; } }



        public Author(int id, string fullName, DateTime birthdate, string description, string website, string twitter, List<string> genres)
        {
            _id = id;
            FullName = fullName;
            Birthdate = birthdate;
            Description = description;
            Website = website;
            Genres = genres;
        }
        public override string ToString()
        {
            return $"{FullName}";
        }
    }
}
