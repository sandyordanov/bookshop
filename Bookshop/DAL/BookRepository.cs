using Classes;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace DAL
{
    public class BookRepository : IBookRepository
    {
        public bool AddBook(Book book)
        {
            using (var connection = new SqlConnection(DbConnectionString.Get()))
            using (var command = connection.CreateCommand())
            {
                // Insert into the base Book table
                command.CommandText = "INSERT INTO Books (Title, Description, Publisher, Language, PublicationDate, Format_id) VALUES (@title, @description, @publisher, @language, @publicationDate, @formatId)";
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@description", book.Description);
                command.Parameters.AddWithValue("@publisher", book.Publisher);
                command.Parameters.AddWithValue("@language", book.Language);
                command.Parameters.AddWithValue("@publicationDate", book.PublicationDate);
                command.Parameters.AddWithValue("@formatId", Convert.ToInt32(book.Format));

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                command.CommandText = "SELECT SCOPE_IDENTITY()";
                int bookId = Convert.ToInt32(command.ExecuteScalar());

                if (book.GetType() == typeof(PaperBook))
                {
                    var paperBook = (PaperBook)book;
                    command.CommandText = "INSERT INTO PaperBooks (Id, Pages, ISBN, ISBN10) VALUES (@id, @pages, @ISBN, @ISBN10)";
                    command.Parameters.AddWithValue("@id", bookId); // Use the ID from the Books table
                    command.Parameters.AddWithValue("@pages", paperBook.Pages);
                    command.Parameters.AddWithValue("@ISBN", paperBook.ISBN);
                    command.Parameters.AddWithValue("@ISBN10", paperBook.ISBN10);

                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result > 0;

                }
                else if (book.GetType() == typeof(EBook))
                {
                    var eBook = (EBook)book;
                    command.CommandText = "INSERT INTO EBooks (Id, Filesize, DownloadLink) VALUES (@id, @filesize, @downloadLink)";
                    command.Parameters.AddWithValue("@id", bookId); // Use the ID from the Books table
                    command.Parameters.AddWithValue("@filesize", eBook.FileSize);
                    command.Parameters.AddWithValue("@ISBN", eBook.DownloadLink);

                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result > 0;

                }

                connection.Close();
                return false;
            }
        }

        public bool UpdateBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                // Update the base Book table
                string bookQuery = "UPDATE Books SET title = @title, description = @description, publisher = @publisher, language = @language, publicationDate = @publicationDate, format = @format WHERE id = @id";
                using (SqlCommand bookCommand = new SqlCommand(bookQuery, connection))
                {
                    bookCommand.Parameters.AddWithValue("id", book.Id);
                    bookCommand.Parameters.AddWithValue("title", book.Title);
                    bookCommand.Parameters.AddWithValue("description", book.Description);
                    bookCommand.Parameters.AddWithValue("publisher", book.Publisher);
                    bookCommand.Parameters.AddWithValue("language", book.Language);
                    bookCommand.Parameters.AddWithValue("publicationDate", book.PublicationDate);
                    bookCommand.Parameters.AddWithValue("format", book.Format);

                    int bookRowsAffected = bookCommand.ExecuteNonQuery();

                    if (bookRowsAffected > 0)
                    {
                        if (book is PaperBook paperBook)
                        {
                            // Update the PaperBook table
                            string paperBookQuery = "UPDATE PaperBooks SET Pages = @pages, ISBN = @ISBN, ISBN10 = @ISBN10 WHERE Id = @id";
                            using (SqlCommand paperBookCommand = new SqlCommand(paperBookQuery, connection))
                            {
                                paperBookCommand.Parameters.AddWithValue("id", paperBook.Id);
                                paperBookCommand.Parameters.AddWithValue("pages", paperBook.Pages);
                                paperBookCommand.Parameters.AddWithValue("ISBN", paperBook.ISBN);
                                paperBookCommand.Parameters.AddWithValue("ISBN10", paperBook.ISBN10);

                                int paperBookRowsAffected = paperBookCommand.ExecuteNonQuery();

                                connection.Close();

                                return paperBookRowsAffected > 0;
                            }
                        }
                        else if (book is EBook eBook)
                        {
                            // Update the EBook table
                            string eBookQuery = "UPDATE EBooks SET FileSize = @fileSize, DownloadLink = @downloadLink WHERE id = @id";
                            using (SqlCommand eBookCommand = new SqlCommand(eBookQuery, connection))
                            {
                                eBookCommand.Parameters.AddWithValue("id", eBook.Id);
                                eBookCommand.Parameters.AddWithValue("fileSize", eBook.FileSize);
                                eBookCommand.Parameters.AddWithValue("downloadLink", eBook.DownloadLink);

                                int eBookRowsAffected = eBookCommand.ExecuteNonQuery();

                                connection.Close();

                                return eBookRowsAffected > 0;
                            }
                        }
                        else
                        {
                            connection.Close();
                            return false; // Unsupported book type
                        }
                    }
                    else
                    {
                        connection.Close();
                        return false;
                    }
                }
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
                    command.Parameters.AddWithValue("id", book.Id);
                    int n = command.ExecuteNonQuery();
                    if (n > 0) { return true; }
                    return false;
                }
            }
        }

        public Book GetBook(int id, Type bookType)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string commonQuery = "SELECT Books.Id, Books.Title, Books.Description, Books.Publisher, Books.Language, Books.PublicationDate, Formats.Format, Authors.Id as 'Author_ID', Authors.FullName, Authors.BirthDate, Authors.Description, Authors.Website, Authors.Twitter";

                int bookId = 0;
                string title = "";
                string description = "";
                string publisher = "";
                string language = "";
                DateOnly publicationDate = default;
                Format format = default;

                // Author information
                int authorId = 0;
                string authorName = "";
                DateOnly authorBirthdate = default;
                string authorDescription = "";
                string authorWebsite = "";
                string authorTwitter = "";

                string query = "";
                switch (bookType.Name)
                {
                    case nameof(PaperBook):
                        query = $"{commonQuery}, PaperBooks.Pages, PaperBooks.ISBN, PaperBooks.ISBN10 " +
                                "FROM Books " +
                                "INNER JOIN Formats ON Books.Format_Id = Formats.Id " +
                                "INNER JOIN Author_Book ON Books.Id = Author_Book.Book_Id " +
                                "INNER JOIN Authors ON Author_Book.Author_id = Authors.Id " +
                                "INNER JOIN PaperBooks ON Books.Id = PaperBooks.Id " +
                                "WHERE Books.Id = @id";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("id", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    bookId = reader.GetInt32(0);
                                    title = reader.GetString(1);
                                    description = reader.GetString(2);
                                    publisher = reader.GetString(3);
                                    language = reader.GetString(4);
                                    publicationDate = (DateOnly)reader.GetValue(5);
                                    format = (Format)reader.GetInt32(6);
                                    authorId = reader.GetInt32(7);
                                    authorName = reader.GetString(8);
                                    authorBirthdate = (DateOnly)reader.GetValue(9);
                                    authorDescription = reader.GetString(10);
                                    authorWebsite = reader.GetString(11);
                                    authorTwitter = reader.GetString(12);
                                    int pages = reader.GetInt32(13);
                                    string isbn = reader.GetString(14);
                                    string isbn10 = reader.GetString(15);
                                    Author author = new Author(authorId, authorName, authorBirthdate, authorDescription, authorWebsite, authorTwitter, null);
                                    List<Author> list = new List<Author> { author };
                                    return new PaperBook(bookId, title, description, publisher, language, publicationDate, format, list, pages, isbn, isbn10);
                                }
                            }
                        }
                        break;

                    case nameof(EBook):
                        query = $"{commonQuery}, Ebooks.Filesize, Ebooks.DownladLink" +
                                "FROM Books " +
                                "INNER JOIN Formats ON Books.Format_Id = Formats.Id " +
                                "INNER JOIN Author_Book ON Books.Id = Author_Book.Book_Id " +
                                "INNER JOIN Authors ON Author_Book.Author_id = Authors.Id " +
                                "INNER JOIN Ebooks ON Books.Id = Ebooks.Id " +
                                "WHERE Books.Id = @id";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("id", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    bookId = reader.GetInt32(0);
                                    title = reader.GetString(1);
                                    description = reader.GetString(2);
                                    publisher = reader.GetString(3);
                                    language = reader.GetString(4);
                                    publicationDate = (DateOnly)reader.GetValue(5);
                                    format = (Format)reader.GetInt32(6);
                                    authorId = reader.GetInt32(7);
                                    authorName = reader.GetString(8);
                                    authorBirthdate = (DateOnly)reader.GetValue(9);
                                    authorDescription = reader.GetString(10);
                                    authorWebsite = reader.GetString(11);
                                    authorTwitter = reader.GetString(12);
                                    double filesize = reader.GetDouble(13);
                                    string link = reader.GetString(14);
                                    Author author = new Author(authorId, authorName, authorBirthdate, authorDescription, authorWebsite, authorTwitter, null);
                                    List<Author> list = new List<Author> { author };
                                    return new EBook(bookId, title, description, publisher, language, publicationDate, format, list, filesize, link);
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }

                connection.Close();
                return null;
            }
        }
        public List<Book> GetAllBooks()
        {
            List<Book> bookList = new List<Book>();

            using (SqlConnection connection = new SqlConnection(DbConnectionString.Get()))
            {
                connection.Open();

                string commonQuery = "SELECT Books.Id, Books.Title, Books.Description, Books.Publisher, Books.Language, Books.PublicationDate, Formats.Format, Authors.Id as 'Author_ID', Authors.FullName, Authors.BirthDate, Authors.Description, Authors.Website, Authors.Twitter";

                string query = $"{commonQuery}, PaperBooks.Pages, PaperBooks.ISBN, PaperBooks.ISBN10, EBooks.FileSize, EBooks.DownloadLink " +
                       "FROM Books " +
                       "INNER JOIN Formats ON Books.Format_Id = Formats.Id " +
                       "INNER JOIN Author_Book ON Books.Id = Author_Book.Book_Id " +
                       "INNER JOIN Authors ON Author_Book.Author_id = Authors.Id " +
                       "LEFT JOIN PaperBooks ON Books.Id = PaperBooks.Id " +
                       "LEFT JOIN EBooks ON Books.Id = EBooks.Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookId = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            string description = reader.GetString(2);
                            string publisher = reader.GetString(3);
                            string language = reader.GetString(4);
                            DateTime dateTime = reader.GetDateTime(5);
                            DateOnly publicationDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                            Format format = (Format)Enum.Parse(typeof(Format), reader.GetString(6), true);

                            int authorId = reader.GetInt32(7);
                            string authorName = reader.GetString(8);
                            DateTime dateAuthor = reader.GetDateTime(9);
                            DateOnly authorBirthdate = new DateOnly(dateAuthor.Year, dateAuthor.Month, dateAuthor.Day);
                            string authorDescription = reader.GetString(10);
                            string authorWebsite = reader.GetString(11);
                            string authorTwitter = reader.GetString(12);

                            Author author = new Author(authorId, authorName, authorBirthdate, authorDescription, authorWebsite, authorTwitter, null);
                            List<Author> authors = new List<Author> { author };

                            if (format == Format.PAPERBOOK || format == Format.HARDCOVER || format == Format.PAPERBACK)
                            {
                                int pages = reader.GetInt32(13);
                                string isbn = reader.GetString(14);
                                string isbn10 = reader.GetString(15);
                                PaperBook paperBook = new PaperBook(bookId, title, description, publisher, language, publicationDate, format, authors, pages, isbn, isbn10);
                                bookList.Add(paperBook);
                            }
                            else if (format == Format.EBOOK)
                            {
                                double fileSize = reader.GetDouble(16);
                                string link = reader.GetString(17);
                                EBook eBook = new EBook(bookId, title, description, publisher, language, publicationDate, format, authors, fileSize, link);
                                bookList.Add(eBook);
                            }
                            else
                            {
                                throw new Exception("An error occured- database error");
                            }
                        }
                    }
                }
            }

            return bookList;
        }

        public void AddAuthor(Author author)
        {
            using (var connection = new SqlConnection(DbConnectionString.Get()))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "INSERT INTO Authors (FullName, BirthDate, Description, Website, Twitter) VALUES (@fullname, @birthdate, @description, @website, @twitter)";
                command.Parameters.AddWithValue("@fullname", author.FullName);
                command.Parameters.AddWithValue("@birthdate", author.Birthdate);
                command.Parameters.AddWithValue("@description", author.Description);
                command.Parameters.AddWithValue("@website", author.Website);
                command.Parameters.AddWithValue("@twitter", author.Twitter);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}