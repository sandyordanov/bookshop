namespace Classes
{
    public class EBook : Book
    {
        public EBook(int id, string title, string author, string language, string publisher, int pages, string ISBN, double price, int publicationYear, List<Review> reviews) : base(id, title, author, language, publisher, pages, ISBN, price, publicationYear, reviews)
        {
        }
    }
}
