using Classes;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class BookRepository : IBookRepository
    {
        public bool AddBook(Book book)
        {
            using (var connection = new SqlConnection(DbConnectionString.Get()))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Books (title, author, language, publisher,  pages, ISBN, price, publicationYear) VALUES (@title, @author, @language, @publisher,  @pages, @ISBN, @price, @publicationYear)";
                command.Parameters.AddWithValue("@title", book.GetTitle());
                command.Parameters.AddWithValue("@author", book.GetAuthor());
                command.Parameters.AddWithValue("@language", book.GetLanguage());
                command.Parameters.AddWithValue("@publisher", book.GetPublisher());
                command.Parameters.AddWithValue("@pages", book.GetPages());
                command.Parameters.AddWithValue("@ISBN", book.GetISBN());
                command.Parameters.AddWithValue("@price", book.GetPrice());
                command.Parameters.AddWithValue("@publicationYear", book.GetPublicationYear());

                connection.Open();
                int n = command.ExecuteNonQuery();
                connection.Close();
                if (n > 0) { return true; }
                return false;
            }

        }

        public bool DeleteBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "DELETE FROM Books WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", book.GetId());
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
                            list.Add(new Book(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetDouble(7), reader.GetInt32(8), new List<Review>()));
                        }
                    }
                }
            }
            return list;
        }

        public Book GetBook(int id)
        {
            Book book;
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "SELECT * FROM Books WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        book = new Book(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6), reader.GetDouble(7), reader.GetInt32(8), new List<Review>());
                    }
                }
            }
            return book;
        }

        public bool UpdateBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();
                string query = "UPDATE Books SET title = @title, author= @author, language= @language, publisher= @publisher,  pages= @pages, ISBN= @Isbn, price= @price, publicationYear= @year WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", book.GetId());
                    command.Parameters.AddWithValue("title", book.GetTitle());
                    command.Parameters.AddWithValue("author", book.GetAuthor());
                    command.Parameters.AddWithValue("language", book.GetLanguage());
                    command.Parameters.AddWithValue("publisher", book.GetPublisher());
                    command.Parameters.AddWithValue("pages", book.GetPages());
                    command.Parameters.AddWithValue("Isbn", book.GetISBN());
                    command.Parameters.AddWithValue("price", book.GetPrice());
                    command.Parameters.AddWithValue("year", book.GetPublicationYear());
                    int n =command.ExecuteNonQuery();
                    connection.Close();
                    if(n > 0) { return true; }
                    return false;
                }
            }
        }
    }
}