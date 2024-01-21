using Classes;
using DAL;

namespace UnitTests
{
    [TestClass]
    public class BookInputValidationTests
    {

        [TestMethod]
        public void Constructor_ValidParameters_BookCreated()
        {
            // Arrange
            int id = 1;
            string title = "Sample Book";
            string description = "Sample Description";
            string publisher = "Sample Publisher";
            string language = "English";
            DateTime publicationDate = DateTime.Now;
            Format format = Format.HARDCOVER;
            List<Author> authors = new List<Author> { new Author()} ;

            // Act
            Book book = new Book(id, title, description, publisher, language, publicationDate, format, authors);

            // Assert
            Assert.IsNotNull(book);
            Assert.AreEqual(id, book.Id);
            Assert.AreEqual(title, book.Title);
            Assert.AreEqual(description, book.Description);
            Assert.AreEqual(publisher, book.Publisher);
            Assert.AreEqual(language, book.Language);
            Assert.AreEqual(publicationDate, book.PublicationDate);
            Assert.AreEqual(format, book.Format);
            Assert.AreEqual(authors, book.Authors);
        }
        [TestMethod]
       
    }
}