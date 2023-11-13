namespace Classes
{
    public class EBook : Book
    {
        public string Format { get; private set; }
        public double FileSize { get; private set; }
        public string DownloadLink { get; private set; }

        public EBook(int id, string title, string description, string author, string language, string format, double fileSize, string downloadLink): base(id, title, description, author, language)
        {
            Format = format;
            FileSize = fileSize;
            DownloadLink = downloadLink;
        }
    }
}
