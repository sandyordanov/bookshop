namespace Classes
{
    public class PaperBook : Book
    {

        int _pages;
        string _iSBN;
        string _iSBN10;

        public int Pages { get => _pages; set => _pages = value; }
        public string ISBN { get => _iSBN; set => _iSBN = value; }
        public string ISBN10 { get => _iSBN10; set => _iSBN10 = value; }

        public PaperBook(int id, string title, string description, string publisher, string language, DateTime publicationDate, Format format, List<Author> authors, int pages, string iSBN, string iSBN10) : base(id, title, description, publisher, language, publicationDate, format, authors)
        {
            Pages = pages;
            ISBN = iSBN;
            ISBN10 = iSBN10;
        }

        public override string ToString()
        {
            return base.ToString() + "{ISBN})";
        }
    }
}