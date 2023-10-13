namespace Classes
{
    public class Review
    {
        int userId;
        int rating;
        string comment;
        DateTime dateCreated;

        public Review(int userId, int rating, string comment)
        {
            this.userId = userId;
            SetRating(rating);
            SetComment(comment);
            dateCreated = SetCreationDate();
        }
        public void SetRating(int rating)
        {
            this.rating = rating;
        }
        public void SetComment(string comment)
        {
            this.comment = comment;
        }
        public DateTime SetCreationDate()
        {
            return DateTime.Now;
        }
        public int GetRating()
        {
            return rating;
        }
        public string GetComment()
        {
            if (!string.IsNullOrEmpty(comment)) { return comment; }
            else { return "No comment added"; }
        }
        public string GetReviewerName()
        {
            return "";
        }
    }
}