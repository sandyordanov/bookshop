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
        private DateOnly _publicationDate;
        private Format _format;
        private List<Author> _authors;
        public int Id { get { return _id; } }
        public string Title { get { return _title; } set { if (value == null) { throw new ArgumentException("Title cannot be null"); } _title = value; } }
        public string Description { get { return _description; } set { if (value == null) { throw new ArgumentException("Description cannot be null"); } _description = value; } }
        public string Publisher { get { return _publisher; } set { if (value == null) { throw new ArgumentException("Publisher cannot be null"); } _publisher = value; } }
        public string Language { get { return _language; } set { if (value == null) { throw new ArgumentException("Language cannot be null"); } _language = value; } }
        public DateOnly PublicationDate { get { return _publicationDate; } set { if (value > DateOnly.MaxValue || value < DateOnly.MinValue) { throw new ArgumentException("Insert valid datetime format"); } _publicationDate = value; } }
        public Format Format { get { return _format; } set { _format = value; } }
        public List<Author> Authors { get { return _authors; } set { if (value == null) { throw new ArgumentException("Authors cannot be null"); } _authors = value; } }

        private ReviewManager reviewManager;

        public Book(int id, string title, string description, string publisher, string language, DateOnly publicationDate, Format format, List<Author> authors)
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

        public override string ToString()
        {
            return $"{Title} - {string.Join(',', Authors.Select(author => author.FullName))}, Publisher: {Publisher} [{Format}]";
        }
    }
}
