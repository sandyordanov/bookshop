
using Classes;

namespace DAL
{
    public interface IBookRepository
    {
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
        public Book GetBook(int id, Type bookType);
        List<Book> GetAllBooks();
        void AddAuthor(Author author);

    }
}
