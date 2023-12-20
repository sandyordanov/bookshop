using Classes;
using DAL;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AddBook_Should_Add_PaperBook_Successfully()
        {
            // Arrange
            BookRepository bookService = new BookRepository();
            PaperBook paperBook = new PaperBook
            {
                Title = "Sample Paper Book",
                Description = "Sample Description",
                Publisher = "Sample Publisher",
                Language = "English",
                PublicationDate = DateTime.Now,
                Format = Format.HARDCOVER,
                Pages = 200,
                ISBN = "1234567890",
                ISBN10 = "0987654321"
            };

            // Act
            bool result = bookService.AddBook(paperBook);

            // Assert
            Assert.IsTrue(result);
            // You may add more assertions here based on your requirements
        }

        [TestMethod]
        public void AddBook_Should_Add_EBook_Successfully()
        {
            // Arrange
            BookRepository bookService = new BookRepository();
            EBook eBook = new EBook
            {
                Title = "Sample EBook",
                Description = "Sample Description",
                Publisher = "Sample Publisher",
                Language = "English",
                PublicationDate = DateTime.Now,
                Format = Format.EBOOK,
                FileSize = 1024, // Size in KB
                DownloadLink = "https://example.com/sample-ebook.pdf"
            };

            // Act
            bool result = bookService.AddBook(eBook);

            // Assert
            Assert.IsTrue(result);
            // You may add more assertions here based on your requirements
        }
    }
}