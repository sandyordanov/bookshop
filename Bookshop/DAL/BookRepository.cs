using Classes;
using DAL.DbConnection;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class BookRepository : IBookRepository
    {
        private string GetConnectionString() => DbConnectionString.Get();

        private void AddAuthorBookRelationship(SqlCommand command, int authorId, int bookId)
        {
            command.CommandText = "INSERT INTO Author_Book (Author_id, Book_id) VALUES (@authorId, @bookId)";
            command.Parameters.AddWithValue("@authorId", authorId);
            command.Parameters.AddWithValue("@bookId", bookId);
            command.ExecuteNonQuery();
        }

        public bool AddBook(Book book)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Books (Title, Description, Publisher, Language, PublicationDate, Format_id) " +
                                          "VALUES (@title, @description, @publisher, @language, @publicationDate, @formatId) SELECT SCOPE_IDENTITY()";
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@description", book.Description);
                    command.Parameters.AddWithValue("@publisher", book.Publisher);
                    command.Parameters.AddWithValue("@language", book.Language);
                    command.Parameters.AddWithValue("@publicationDate", book.PublicationDate);
                    command.Parameters.AddWithValue("@formatId", Convert.ToInt32(book.Format));

                    int bookId = Convert.ToInt32(command.ExecuteScalar());

                    foreach (var author in book.Authors)
                    {
                        AddAuthorBookRelationship(command, author.Id, bookId);
                    }

                    if (book is PaperBook paperBook)
                    {
                        command.CommandText = "INSERT INTO PaperBooks (Id, Pages, ISBN, ISBN10) " +
                                              "VALUES (@id, @pages, @ISBN, @ISBN10)";
                        command.Parameters.AddWithValue("@id", bookId);
                        command.Parameters.AddWithValue("@pages", paperBook.Pages);
                        command.Parameters.AddWithValue("@ISBN", paperBook.ISBN);
                        command.Parameters.AddWithValue("@ISBN10", paperBook.ISBN10);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                    else if (book is EBook eBook)
                    {
                        command.CommandText = "INSERT INTO EBooks (Id, Filesize, DownloadLink) " +
                                              "VALUES (@id, @filesize, @downloadLink)";
                        command.Parameters.AddWithValue("@id", bookId);
                        command.Parameters.AddWithValue("@filesize", eBook.FileSize);
                        command.Parameters.AddWithValue("@downloadLink", eBook.DownloadLink);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            return false;
        }

        public bool UpdateBook(Book book)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (var bookCommand = connection.CreateCommand())
                {
                    bookCommand.CommandText = "UPDATE Books SET title = @title, description = @description, " +
                                              "publisher = @publisher, language = @language, " +
                                              "publicationDate = @publicationDate, format_Id = @format " +
                                              "WHERE id = @id";
                    bookCommand.Parameters.AddWithValue("@id", book.Id);
                    bookCommand.Parameters.AddWithValue("@title", book.Title);
                    bookCommand.Parameters.AddWithValue("@description", book.Description);
                    bookCommand.Parameters.AddWithValue("@publisher", book.Publisher);
                    bookCommand.Parameters.AddWithValue("@language", book.Language);
                    bookCommand.Parameters.AddWithValue("@publicationDate", book.PublicationDate);
                    bookCommand.Parameters.AddWithValue("@format", Convert.ToInt32(book.Format));

                    int bookRowsAffected = bookCommand.ExecuteNonQuery();

                    if (bookRowsAffected > 0)
                    {
                        if (book is PaperBook paperBook)
                        {
                            using (var paperBookCommand = connection.CreateCommand())
                            {
                                paperBookCommand.CommandText = "UPDATE PaperBooks SET Pages = @pages, " +
                                                               "ISBN = @ISBN, ISBN10 = @ISBN10 WHERE Id = @id";
                                paperBookCommand.Parameters.AddWithValue("@id", paperBook.Id);
                                paperBookCommand.Parameters.AddWithValue("@pages", paperBook.Pages);
                                paperBookCommand.Parameters.AddWithValue("@ISBN", paperBook.ISBN);
                                paperBookCommand.Parameters.AddWithValue("@ISBN10", paperBook.ISBN10);

                                int paperBookRowsAffected = paperBookCommand.ExecuteNonQuery();
                                return paperBookRowsAffected > 0;
                            }
                        }
                        else if (book is EBook eBook)
                        {
                            using (var eBookCommand = connection.CreateCommand())
                            {
                                eBookCommand.CommandText = "UPDATE EBooks SET FileSize = @fileSize, " +
                                                           "DownloadLink = @downloadLink WHERE id = @id";
                                eBookCommand.Parameters.AddWithValue("@id", eBook.Id);
                                eBookCommand.Parameters.AddWithValue("@fileSize", eBook.FileSize);
                                eBookCommand.Parameters.AddWithValue("@downloadLink", eBook.DownloadLink);

                                int eBookRowsAffected = eBookCommand.ExecuteNonQuery();
                                return eBookRowsAffected > 0;
                            }
                        }
                        else
                        {
                            return false; // Unsupported book type
                        }
                    }
                    return false;
                }
            }
        }

        public bool DeleteBook(Book book)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                string query = "DELETE FROM Books WHERE id = @id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", book.Id);
                    int n = command.ExecuteNonQuery();
                    return n > 0;
                }
            }
        }

        public Book GetBook(int bookId)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string commonQuery = "SELECT Books.Id, Books.Title, Books.Description, Books.Publisher, Books.Language, " +
                                               "Books.PublicationDate, Formats.Format, Authors.Id as 'Author_ID', Authors.FullName, " +
                                               "Authors.BirthDate, Authors.Description, Authors.Website, Authors.Twitter " +
                                               "FROM Books " +
                                               "INNER JOIN Formats ON Books.Format_Id = Formats.Id " +
                                               "INNER JOIN Author_Book ON Books.Id = Author_Book.Book_Id " +
                                               "INNER JOIN Authors ON Author_Book.Author_id = Authors.Id " +
                                               "WHERE Books.Id = @id";

                        using (var command = new SqlCommand(commonQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", bookId);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int BookId = reader.GetInt32(0);
                                    string Title = reader.GetString(1);
                                    string Description = reader.GetString(2);
                                    string Publisher = reader.GetString(3);
                                    string Language = reader.GetString(4);
                                    DateTime PublicationDate = (DateTime)reader.GetValue(5);
                                    Format format = (Format)Enum.Parse(typeof(Format), reader.GetString(6), true);

                                    int AuthorId = reader.GetInt32(7);
                                    string AuthorName = reader.GetString(8);
                                    DateTime AuthorBirthdate = (DateTime)reader.GetValue(9);
                                    string AuthorDescription = reader.GetString(10);
                                    string AuthorWebsite = reader.GetString(11);
                                    string AuthorTwitter = reader.GetString(12);

                                    Author author = new Author(AuthorId, AuthorName, AuthorBirthdate, AuthorDescription, AuthorWebsite, AuthorTwitter);
                                    List<Author> authors = new List<Author> { author };
                                    reader.Close();
                                    switch (format)
                                    {
                                        case Format.PAPERBOOK:
                                        case Format.PAPERBACK:
                                        case Format.HARDCOVER:
                                            // Fetch additional PaperBook properties
                                            string paperBookQuery = "SELECT Pages, ISBN, ISBN10 FROM PaperBooks WHERE Id = @id";
                                            using (var paperBookCommand = new SqlCommand(paperBookQuery, connection, transaction))
                                            {
                                                paperBookCommand.Parameters.AddWithValue("@id", BookId);
                                                using (var paperBookReader = paperBookCommand.ExecuteReader())
                                                {
                                                    if (paperBookReader.Read())
                                                    {
                                                        int pages = paperBookReader.GetInt32(0);
                                                        string isbn = paperBookReader.GetString(1);
                                                        string isbn10 = paperBookReader.GetString(2);

                                                        return new PaperBook(bookId, Title, Description, Publisher, Language, PublicationDate, format, authors, pages, isbn, isbn10);
                                                    }
                                                }
                                            }
                                            break;

                                        case Format.EBOOK:
                                            // Fetching additional EBook properties
                                            string ebookQuery = "SELECT Filesize, DownloadLink FROM Ebooks WHERE Id = @id";
                                            using (var ebookCommand = new SqlCommand(ebookQuery, connection, transaction))
                                            {
                                                ebookCommand.Parameters.AddWithValue("@id", BookId);
                                                using (var ebookReader = ebookCommand.ExecuteReader())
                                                {
                                                    if (ebookReader.Read())
                                                    {
                                                        double fileSize = ebookReader.GetDouble(0);
                                                        string downloadLink = ebookReader.GetString(1);

                                                        return new EBook(bookId, Title, Description, Publisher, Language, PublicationDate, format, authors, fileSize, downloadLink);
                                                    }
                                                }
                                            }
                                            break;

                                        default:
                                            // Handle unsupported formats or other cases
                                            break;
                                    }
                                }
                            }
                        }

                        // Commit the transaction if successful
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions and rollback the transaction if an error occurs
                        transaction.Rollback();
                        // Log or throw the exception as needed
                        throw;
                    }
                }
            }

            return null;
        }


        public List<Book> GetAllBooks()
        {
            List<Book> bookList = new List<Book>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                string commonQuery = "SELECT Books.Id, Books.Title, Books.Description, Books.Publisher, " +
                                     "Books.Language, Books.PublicationDate, Formats.Format, " +
                                     "Authors.Id as 'Author_ID', Authors.FullName, Authors.BirthDate, " +
                                     "Authors.Description, Authors.Website, Authors.Twitter";

                string query = $"{commonQuery}, PaperBooks.Pages, PaperBooks.ISBN, PaperBooks.ISBN10, " +
                               "EBooks.FileSize, EBooks.DownloadLink " +
                               "FROM Books " +
                               "INNER JOIN Formats ON Books.Format_Id = Formats.Id " +
                               "INNER JOIN Author_Book ON Books.Id = Author_Book.Book_Id " +
                               "INNER JOIN Authors ON Author_Book.Author_id = Authors.Id " +
                               "LEFT JOIN PaperBooks ON Books.Id = PaperBooks.Id " +
                               "LEFT JOIN EBooks ON Books.Id = EBooks.Id";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookId = reader.GetInt32(0);
                            string title = reader.GetString(1);
                            string description = reader.GetString(2);
                            string publisher = reader.GetString(3);
                            string language = reader.GetString(4);
                            DateTime publicationDate = reader.GetDateTime(5);
                            Format format = (Format)Enum.Parse(typeof(Format), reader.GetString(6), true);

                            int authorId = reader.GetInt32(7);
                            string authorName = reader.GetString(8);
                            DateTime authorBirthdate = reader.GetDateTime(9);
                            string authorDescription = reader.GetString(10);
                            string authorWebsite = reader.GetString(11);
                            string authorTwitter = reader.GetString(12);

                            Author author = new Author(authorId, authorName, authorBirthdate, authorDescription, authorWebsite, authorTwitter);
                            List<Author> authors = new List<Author> { author };

                            if (format == Format.PAPERBOOK || format == Format.HARDCOVER || format == Format.PAPERBACK)
                            {
                                int pages = reader.GetInt32(13);
                                string isbn = reader.GetString(14);
                                string isbn10 = reader.GetString(15);
                                Book paperBook = new PaperBook(bookId, title, description, publisher, language, publicationDate, format, authors, pages, isbn, isbn10);
                                bookList.Add(paperBook);
                            }
                            else if (format == Format.EBOOK)
                            {
                                double fileSize = reader.GetDouble(16);
                                string link = reader.GetString(17);
                                Book eBook = new EBook(bookId, title, description, publisher, language, publicationDate, format, authors, fileSize, link);
                                bookList.Add(eBook);
                            }
                            else
                            {
                                
                                throw new Exception("An error occurred - unsupported book type");
                            }
                        }
                    }
                }
            }
            return bookList;
        }

        public void AddAuthor(Author author)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Authors (FullName, BirthDate, Description, Website, Twitter) " +
                                          "VALUES (@fullname, @birthdate, @description, @website, @twitter)";
                    command.Parameters.AddWithValue("@fullname", author.FullName);
                    command.Parameters.AddWithValue("@birthdate", author.Birthdate);
                    command.Parameters.AddWithValue("@description", author.Description);
                    command.Parameters.AddWithValue("@website", author.Website);
                    command.Parameters.AddWithValue("@twitter", author.Twitter);
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Author> GetAllAuthors()
        {
            List<Author> authors = new List<Author>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, FullName, BirthDate, Description, Website, Twitter FROM Authors";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int authorId = reader.GetInt32(0);
                            string authorName = reader.GetString(1);
                            DateTime authorBirthdate = reader.GetDateTime(2);
                            string authorDescription = reader.GetString(3);
                            string authorWebsite = reader.GetString(4);
                            string authorTwitter = reader.GetString(5);

                            Author author = new Author(authorId, authorName, authorBirthdate, authorDescription, authorWebsite, authorTwitter);
                            authors.Add(author);
                        }
                    }
                }

                connection.Close();
            }

            return authors;
        }

        public List<Format> GetAllFormats()
        {
            List<Format> formats = new List<Format>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id FROM Formats";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Format format = (Format)reader.GetInt32(0);
                            formats.Add(format);
                        }
                    }
                }

                connection.Close();
            }

            return formats;
        }
    }
}