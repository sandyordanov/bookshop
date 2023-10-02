namespace Classes
{
    public class Book
    {
        int id;
        string title;
        string author;
        string language;
        string publisher;
        int pages;
        string ISBN;
        double price;
        int publicationYear;
        private List<Review> reviews;

       
        public Book(int id, string title, string author, string language, string publisher, int pages, string ISBN, double price, int publicationYear, List<Review> reviews)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.language = language;
            this.publisher = publisher;
            this.pages = pages;
            this.ISBN = ISBN;
            this.price = price;
            this.publicationYear = publicationYear;
            this.reviews = reviews;
        }
        public Book(string title, string author, string language, string publisher, int pages, string ISBN, double price, int publicationYear, List<Review> reviews)
        {
            this.title = title;
            this.author = author;
            this.language = language;
            this.publisher = publisher;
            this.pages = pages;
            this.ISBN = ISBN;
            this.price = price;
            this.publicationYear = publicationYear;
            this.reviews = reviews;
        }

        public void SetId(int id) { this.id = id;}
        public void SetTitle(string title) {  this.title = title;}
        public void SetAuthor(string author) {  this.author = author; }
        public void SetPublisher(string publisher) {  this.publisher = publisher; }
        public void SetLanguage(string language) { this.language = language;}
        public void SetPages(int pages) {  this.pages = pages; }
        public void SetISBN(string ISBN) {  this.ISBN = ISBN; }
        public void SetPrice(double price) {  this.price = price; }
        public void SetPublicationYear(int year) {  this.publicationYear = year; }

        public int GetId() { return id; }
        public string GetTitle() { return this.title;}
        public string GetAuthor() { return this.author;}
        public string GetLanguage() {  return this.language;}
        public string GetPublisher() { return this.publisher;}
        public int GetPages() { return this.pages;}
        public string GetISBN() {  return this.ISBN;}
        public double GetPrice() { return this.price;}
        public int GetPublicationYear() {  return this.publicationYear;}

        public void AddReview(Review review)
        {

        }
        public List<Review> GetReviews() {  return this.reviews; }

        public override string ToString()
        {
            return $"{title} - {author}, Publisher: {publisher}({ISBN})";
        }
    }
}