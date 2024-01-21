namespace Classes
{
    public class PaperBook : Book
    {

        int _pages;
        string _iSBN;
        string _iSBN10;

        public int Pages
        {
            get => _pages;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Page count must be positive.");
                _pages = value;
            }
        }
        public string ISBN
        {
            get => _iSBN;
            set
            {
                if (!IsValidISBN(value))
                    throw new ArgumentException("Invalid ISBN format. ISBN must be 10 or 13 digits long.", nameof(value));
                _iSBN = value;
            }
        }
        public string ISBN10
        {
            get => _iSBN10;
            set
            {
                if (value.Length != 10 || !value.All(char.IsDigit))
                    throw new ArgumentException("Invalid ISBN10 format. It must be 10 digits long.", nameof(value));
                _iSBN10 = value;
            }
        }
        public PaperBook()
        {
           
        }
        public PaperBook(int id, string title, string description, string publisher, string language, DateTime publicationDate, Format format, List<Author> authors, int pages, string iSBN, string iSBN10) : base(id, title, description, publisher, language, publicationDate, format, authors)
        {
            Pages = pages;
            ISBN = iSBN;
            ISBN10 = iSBN10;
        }
        private bool IsValidISBN(string isbn)
        {
            return (isbn.Length == 10 || isbn.Length == 13) && isbn.All(char.IsDigit);
        }

        public override string ToString()
        {
            return base.ToString() + "{ISBN})";
        }
    }
}