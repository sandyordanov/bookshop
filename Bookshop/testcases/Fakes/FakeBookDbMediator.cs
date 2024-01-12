using Classes;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Fakes
{
    public class FakeBookDbMediator : IBookRepository
    {
        private Dictionary<int, Book> books;
        private Dictionary<int, Author> authors;
        public FakeBookDbMediator()
        {
            books = new Dictionary<int, Book>();
            authors = new Dictionary<int, Author>();
            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "0293829311", "1233124212");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(2, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");
            books.Add(book1.Id, book1);
            books.Add(book2.Id, book2);
            authors.Add(1, new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1"));
            authors.Add(2, new Author(2, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1"));
        }
        public void AddAuthor(Author author)
        {
            authors.Add(author.Id, author);
        }

        public bool AddBook(Book book)
        {
            books.Add(book.Id, book);
            return true;
        }

        public bool DeleteBook(Book book)
        {
            books.Remove(book.Id);
            return true;
        }

        public List<Author> GetAllAuthors()
        {
            return authors.Values.ToList();
        }

        public List<Book> GetAllBooks()
        {
            return books.Values.ToList();
        }

        public List<Format> GetAllFormats()
        {
            var formats = new List<Format>() { Format.EBOOK, Format.PAPERBOOK, Format.AUDIOBOOK, Format.PAPERBACK, Format.HARDCOVER };
            return formats;
        }

        public Book GetBook(int id)
        {
            return books[id];
        }

        public bool UpdateBook(Book book)
        {
            books[book.Id] = book;
            return true;
        }
    }
}
