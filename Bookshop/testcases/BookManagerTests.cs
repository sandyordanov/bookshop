using BLL;
using BLL.StrategyFilters;
using Classes;
using DAL;
using DAL.Interfaces;
using Moq;
using UnitTests.Fakes;

namespace UnitTests
{
    [TestClass]
    public class BookManagerTests
    {
        IBookRepository bookRepo = new FakeBookDbMediator();
        IReviewRepository reviewRepo = new FakeReviewDbMediator();


        [TestMethod]
        public void SearchEngineShouldReturnCorrectResults()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var result = manager.SearchForBooks("The Great Book", manager.GetAllBooks());
            //Assert
            Assert.AreEqual(result.Count, 2);
        }
        [TestMethod]
        public void SearchEngineShouldReturnEmptyList()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var result = manager.SearchForBooks("NewBook", manager.GetAllBooks());
            //Assert
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void SearchEngineShouldReturnCorrectResultsByAuthor()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var result = manager.SearchForBooks("Robert Brown", manager.GetAllBooks());
            //Assert
            Assert.AreEqual(result.Count, 1);
        }
        [TestMethod]
        public void FilterByStrategies_Should_FilterByAuthor()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var result = manager.FilterByStrategies(new List<IBookFilterStrategy>() { new AuthorFilterStrategy(new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1")) });
            //Assert
            Assert.AreEqual(result.Count(), 1);
        }
        [TestMethod]
        public void FilterByStrategies_Should_FilterByPaperBookType()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var result = manager.FilterByStrategies(new List<IBookFilterStrategy>() { new PaperBookFilterStrategy() });
            //Assert
            Assert.AreEqual(result.Count(), 1);
        }
        [TestMethod]
        public void FilterByStrategies_Should_FilterByEBookType()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var result = manager.FilterByStrategies(new List<IBookFilterStrategy>() { new EbookFilterStrategy() });
            //Assert
            Assert.AreEqual(result.Count(), 1);
        }
        [TestMethod]
        public void GetBookStatistics_Should_ReturnCorrectStatistics()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var book = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1231231232", "1231231231");
            var result = manager.GetBookStatistics(book);
            //Assert
            Assert.AreEqual(result.fiveStarReviewsCount, 1);
            Assert.AreEqual(result.fourStarReviewsCount, 0);
            Assert.AreEqual(result.threeStarReviewsCount, 0);
            Assert.AreEqual(result.Average, 5);
        }
        [TestMethod]
        public void GetBookStatistics_Should_ReturnCorrectStatisticsForEBook()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var book = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(2, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");
            var result = manager.GetBookStatistics(book);
            //Assert
            Assert.AreEqual(result.fiveStarReviewsCount, 0);
            Assert.AreEqual(result.twoStarReviewsCount, 1);
            Assert.AreEqual(result.threeStarReviewsCount, 0);
            Assert.AreEqual(result.Average, 2);
        }
        [TestMethod]
        public void SortBooksByRating_Should_ReturnCorrectOrder()
        {
            //Arrange
            BookManager manager = new BookManager(bookRepo, reviewRepo);
            //Act
            var result = manager.SortBooksByRating(manager.GetAllBooks());
            //Assert
            Assert.AreEqual(result.FirstOrDefault().Key.Title, "The Great Book1");
        }
    }
}