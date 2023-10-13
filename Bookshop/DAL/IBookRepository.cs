
using Classes;

namespace DAL
{
    public interface IBookRepository
    {
        bool AddBook(PaperBook book);
        bool UpdateBook(PaperBook book);
        bool DeleteBook(PaperBook book);
        PaperBook GetBook(int id);
        List<PaperBook> GetAllBooks();
        bool AddReview(Book book, Review review);
        bool RemoveReview(Book book, Review review);
        List<Review> GetReviewsByBook(Book book);
    }
}
