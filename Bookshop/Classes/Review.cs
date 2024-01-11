using System.ComponentModel.DataAnnotations;

namespace Classes
{
    public class Review
    {
        int _id;
        string _comment;
        int _rating;
        DateTime _date;
        int _likes;
        private User _user;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [Required(ErrorMessage = "Comment is required.")]
        [MaxLength(5000)]
        public string Comment
        {
            get => _comment;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Comment cannot be empty or whitespace.");
                }
                _comment = value;
            }
        }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentOutOfRangeException("Rating must be between 1 and 5.");
                }
                _rating = value;
            }
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public int Likes
        {
            get => _likes;
            set
            {
                
                _likes = value;
            }
        }
        public User User { get => _user; set => _user = value; }
        public Book Book { get; set; }
        public Review(int id, string comment, int rating, DateTime date, int likes, User user, Book book)
        {
            _id = id;
            Comment = comment;
            Rating = rating;
            Date = date;
            Likes = likes;
            User = user;
            Book = book;
        }
        public Review()
        {

        }
        public override string ToString()
        {
            return $"Review id: {Id}, user: {User.Username}, book: {Book.Title}";
        }
    }
}