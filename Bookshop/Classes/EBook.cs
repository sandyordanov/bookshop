namespace Classes
{
    public class EBook : Book
    {
        private double _filesize;
        private string _downloadLink;

        public double FileSize
        {
            get => _filesize;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "File size must be a positive number.");
                _filesize = value;
            }
        }
        public string DownloadLink
        {
            get => _downloadLink;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Invalid URL format.", nameof(value));
                _downloadLink = value;
            }
        }
        public EBook()
        {
            
        }
        public EBook(int id, string title, string description, string publisher, string language, DateTime publicationDate, Format format, List<Author> authors, double filesize, string downloadLink) : base(id, title, description, publisher, language, publicationDate, format, authors)
        {
            FileSize = filesize;
            DownloadLink = downloadLink;
        }
    }
}
