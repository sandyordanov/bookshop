namespace Classes
{
    public class Review
    {
        int _id;
        string _comment;
        int _rating;
        DateOnly _date;
        int _likes;
        private User _user;
        public int Id { get { return _id; } }
        public string Comment { get { return _comment; } set { _comment = value; } }
        public int Rating { get { return _rating; } set { _rating = value; } }
        public DateOnly Date { get { return _date; } set { _date = value; } }
        public int Likes { get { return _likes; } set { _likes = value; } }
        public User User { get => _user; set => _user = value; }

        public Review(int id, string comment, int rating, DateOnly date, int likes, User user)
        {
            _id = id;
            Comment = comment;
            Rating = rating;
            Date = date;
            Likes = likes;
            User = user;
        }
    }
}