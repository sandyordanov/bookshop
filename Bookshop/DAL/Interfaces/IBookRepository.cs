
using Classes;

namespace DAL.Interfaces
{
    public interface IBookRepository
    {
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
        public Book GetBook(int id, Type bookType);
        List<Book> GetAllBooks();
        void AddAuthor(Author author);
        List<Author> GetAllAuthors();
        List<Format> GetAllFormats();
    }
}
