
using Classes;

namespace DAL
{
    public interface IBookRepository
    {
        bool AddBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
        Book GetBook(int id);
        List<Book> GetAllBooks();
    }
}
