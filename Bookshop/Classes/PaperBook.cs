namespace Classes
{
    public class PaperBook : Book
    {
        string publisher;
        int pages;
        string iSBN;
        int publicationYear;

        public PaperBook(int id, string title, string description, string author, string language, string publisher, int pages, string ISBN, int publicationYear) : base(id, title, description, author, language)
        {
            this.publisher = publisher;
            this.pages = pages;
            iSBN = ISBN;
            this.publicationYear = publicationYear;
        }

        public string Publisher { get { return publisher; } set { publisher = value; } }
        public int Pages { get { return pages; } set { pages = value; } }
        public string ISBN { get { return iSBN; } set { iSBN = value; } }
        public int PublicationYear { get { return publicationYear; } set { publicationYear = value; } }

        public void SetPublisher(string _publisher) { publisher = _publisher; }
        public void SetPages(int _pages) { pages = _pages; }
        public void SetISBN(string _ISBN) { ISBN = _ISBN; }
        public void SetPublicationYear(int year) { publicationYear = year; }
        public string GetPublisher() { return publisher; }
        public int GetPages() { return pages; }
        public string GetISBN() { return ISBN; }
        public int GetPublicationYear() { return publicationYear; }

        public override string ToString()
        {
            return $"{Title} - {Author}, Publisher: {publisher}({ISBN})";
        }
    }
}