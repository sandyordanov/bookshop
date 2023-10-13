namespace Classes
{
    public class PaperBook : Book
    {
        string publisher;
        int pages;
        string iSBN;       
        int publicationYear;
        int quantity;

        public PaperBook(int id, string title, double price, string description, string author, string language, string publisher, int pages, string ISBN, int publicationYear, int quantity) : base(id, title, price, description, author, language)
        {
            this.publisher = publisher;
            this.pages = pages;
            this.ISBN = ISBN;
            this.publicationYear = publicationYear;
            this.quantity = quantity;
        }
         public PaperBook( string title, double price, string description, string author, string language, string publisher, int pages, string ISBN, int publicationYear, int quantity) : base( title, price, description, author, language)
        {
            this.publisher = publisher;
            this.pages = pages;
            this.ISBN = ISBN;
            this.publicationYear = publicationYear;
            this.quantity = quantity;
        }
        public PaperBook()
        {
        }

        public string Publisher { get { return publisher; } set { this.publisher = value; } }
        public int Pages { get {return pages; } set { this.pages = value; } }
        public string ISBN { get { return this.iSBN; } set { iSBN = value; } }
        public int PublicationYear { get { return this.publicationYear; } set { this.publicationYear = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }

        public void SetPublisher(string publisher) {  this.publisher = publisher; }
        public void SetPages(int pages) {  this.pages = pages; }
        public void SetISBN(string ISBN) {  this.ISBN = ISBN; }
        public void SetPublicationYear(int year) {  this.publicationYear = year; }
        public void SetQuantity(int quantity) { this.quantity = quantity;}
        public string GetPublisher() { return this.publisher;}
        public int GetPages() { return this.pages;}
        public string GetISBN() {  return this.ISBN;}
        public int GetPublicationYear() {  return this.publicationYear;}
        public int GetQuantity() { return this.quantity;}
      
        public override string ToString()
        {
            return $"{base.Title} - {base.Author}, Publisher: {publisher}({ISBN})";
        }
    }
}