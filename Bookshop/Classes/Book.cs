using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Book
    {
        int id;
        string title;
        double price;
        string description;
        string author;
        string language;
        private List<Review> reviews;

        public Book(int id, string title, double price, string description, string author, string language)
        {
            this.id = id;
            this.title = title;
            this.price = price;
            this.description = description;
            this.author = author;
            this.language = language;
            reviews = new List<Review>();
        }
        public Book( string title, double price, string description, string author, string language)
        {
            this.title = title;
            this.price = price;
            this.description = description;
            this.author = author;
            this.language = language;
            reviews = new List<Review>();
        }
        public Book()
        {
        }
        public int Id { get { return id; } set { id = value; } }
        public string Title { get {  return title; } set {  title = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string Author { get { return author; } set { author = value; } }
        public string Language { get { return language; } set { language = value; } }

        public void AddReview(Review review)
        {
            reviews.Add(review);
        }
        public void RemoveReview(Review review)
        {
            reviews.Remove(review);
        }
        public List<Review> GetReviews() { return this.reviews; }


    }
}
