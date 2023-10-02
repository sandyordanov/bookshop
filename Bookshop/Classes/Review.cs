namespace Classes
{
    public class Review
    {
        int rating;
        string? comment;
        string? reviewerName;
        DateTime dateCreated;

        public Review(int rating, string comment, string name)
        {
            SetRating(rating);
            SetComment(comment);
            SetReviewerName(name);
            SetCreationDate();
        }
        public void SetRating(int rating)
        {
            this.rating = rating;
        }
        public void SetComment(string comment)
        {
            this.comment = comment;
        }
        public void SetReviewerName(string reviewerName)
        {
            if (reviewerName != null)
            {
                this.reviewerName = reviewerName;
                return;
            }
            else
            {
                this.reviewerName = "anonymous";
            }
        }
        public void SetCreationDate()
        {
            dateCreated = DateTime.Now;
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
            return reviewerName;
        }
    }
}