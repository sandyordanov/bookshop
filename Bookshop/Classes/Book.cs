using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Classes
{
    public abstract class Book
    {
        private int _id;
        private string _title;
        private string _description;
        private string _publisher;
        private string _language;
        private DateTime _publicationDate;
        private Format _format;
        private List<Author> _authors;
        public int Id { get { return _id; } }
        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be null or empty");
                }
                if (value.Length > 100)
                {
                    throw new ArgumentException("Title cannot exceed 100 characters");
                }
                if (!value.Any(char.IsLetter))
                {
                    throw new ArgumentException("Title must contain at least one alphabetic character");
                }
                _title = value;
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be null or empty");
                }
                if (value.Length > 1000)
                {
                    throw new ArgumentException("Description cannot exceed 1000 characters");
                }
                _description = value;
            }
        }
        public string Publisher
        {
            get { return _publisher; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Publisher cannot be null or empty");
                }
                if (value.Length > 100)
                {
                    throw new ArgumentException("Publisher cannot exceed 25 characters");
                }
                if (!value.Any(char.IsLetterOrDigit))
                {
                    throw new ArgumentException("Publisher must contain at least one alphanumeric character");
                }
                _publisher = value;
            }
        }
        public string Language
        {
            get { return _language; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Language cannot be null or empty");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("Language cannot exceed 50 characters");
                }
                if (!value.All(char.IsLetter))
                {
                    throw new ArgumentException("Language must consist only of alphabetic characters");
                }
                _language = value;
            }
        }

        public DateTime PublicationDate { get { return _publicationDate; } set { if (value > DateTime.MaxValue || value < DateTime.MinValue) { throw new ArgumentException("Insert valid datetime format"); } _publicationDate = value; } }
        public Format Format { get { return _format; } set { _format = value; } }
        public List<Author> Authors { get { return _authors; } set { if (value == null) { throw new ArgumentException("Authors cannot be null"); } _authors = value; } }
        public string Genre { get; set; }
        private ReviewManager reviewManager;
        public Book()
        {
            
        }
        public Book(int id, string title, string description, string publisher, string language, DateTime publicationDate, Format format, List<Author> authors)
        {
            _id = id;
            Title = title;
            Description = description;
            Publisher = publisher;
            Language = language;
            PublicationDate = publicationDate;
            Format = format;
            Authors = authors;
            reviewManager = new ReviewManager();
        }

        public void AddReview(Review review)
        {
            reviewManager.AddReview(review);
        }
        public void RemoveReview(Review review)
        {
            reviewManager.RemoveReview(review);
        }
        public List<Review> GetReviews()
        {
            return reviewManager.GetReviews();
        }
        public Statistics GetStatistics() { return reviewManager.GetStatistics();}
        public void LikeReview(int reviewId)
        {
            reviewManager.LikeReview(reviewId);
        }
        public override string ToString()
        {
            return $"{Title} - {string.Join(',', Authors.Select(author => author.FullName))}, Publisher: {Publisher} [{Format}]";
        }
    }
}
