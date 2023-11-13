using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Book
    {
        public int? Id { get; set; }
        public string Title { get;  set; }
        public string Author { get;  set; }
        public string Description { get; set; }
        public string Language { get;  set; }
        private ReviewManager reviewManager;

        public Book(int id, string title, string description, string author, string language)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
            Language = language;
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
    }
}
