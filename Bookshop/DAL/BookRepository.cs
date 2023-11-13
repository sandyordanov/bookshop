using Classes;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class BookRepository : IBookRepository
    {
        public bool AddBook(PaperBook book)
        {
            using (var connection = new SqlConnection(DbConnectionString.Get()))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Books (title, author, language, publisher,  pages, ISBN, publicationYear) VALUES (@title, @author, @language, @publisher,  @pages, @ISBN, @publicationYear)";
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@language", book.Language);
                command.Parameters.AddWithValue("@publisher", book.GetPublisher());
                command.Parameters.AddWithValue("@pages", book.GetPages());
                command.Parameters.AddWithValue("@ISBN", book.GetISBN());
                command.Parameters.AddWithValue("@publicationYear", book.GetPublicationYear());

                connection.Open();
                int n = command.ExecuteNonQuery();
                connection.Close();
                if (n > 0) { return true; }
                return false;
            }

        }

        public bool AddReview(Book book, Review review)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBook(PaperBook book)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "DELETE FROM Books WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", book.Id);
                    int n = command.ExecuteNonQuery();
                    if (n > 0) { return true; }
                    return false;
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>();
            using (var connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "SELECT * FROM Books";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(
                                new Book(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4)));



                        }
                    }
                }
            }
            return list;
        }

        public PaperBook GetBook(int id)
        {
            PaperBook book;
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "SELECT * FROM Books WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        book = new PaperBook()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Language = reader.GetString(3),
                            Publisher = reader.GetString(4),
                            Pages = reader.GetInt32(5),
                            ISBN = reader.GetString(6),
                            PublicationYear = reader.GetInt32(8),
                            Description = ""
                        };
                        return book;
                    }
                }
            }

        }

        public List<Review> GetReviewsByBook(Book book)
        {
            throw new NotImplementedException();
        }

        public bool RemoveReview(Book book, Review review)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBook(PaperBook book)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "UPDATE Books SET title = @title, author= @author, language= @language, publisher= @publisher,  pages= @pages, ISBN= @Isbn, publicationYear= @year WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", book.Id);
                    command.Parameters.AddWithValue("title", book.Title);
                    command.Parameters.AddWithValue("author", book.Author);
                    command.Parameters.AddWithValue("language", book.Language);
                    command.Parameters.AddWithValue("publisher", book.GetPublisher());
                    command.Parameters.AddWithValue("pages", book.GetPages());
                    command.Parameters.AddWithValue("Isbn", book.GetISBN());
                    command.Parameters.AddWithValue("year", book.GetPublicationYear());
                    int n = command.ExecuteNonQuery();
                    connection.Close();
                    if (n > 0) { return true; }
                    return false;
                }
            }
        }

        List<PaperBook> IBookRepository.GetAllBooks()
        {
            throw new NotImplementedException();
        }
    }
}