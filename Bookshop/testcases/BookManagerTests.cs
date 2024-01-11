using BLL;
using BLL.StrategyFilters;
using Classes;
using DAL;
using DAL.Interfaces;
using UnitTests.Fakes;

namespace UnitTests
{
    [TestClass]
    public class BookManagerTests
    {
        [TestMethod]
        public void FilterByStrategies_Should_FilterByAuthor()
        {
            //Arrange
            IBookRepository bookRepo = new FakeBookDbMediator();



            IReviewRepository reviewRepo = new FakeReviewDbMediator();
            BookManager manager = new BookManager(bookRepo, reviewRepo);

            Author author = new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" });

            bookRepo.AddAuthor(author);
            List<IBookFilterStrategy> strategies = new List<IBookFilterStrategy>() { new AuthorFilterStrategy(author) };
            //Act
            var result =manager.FilterByStrategies(strategies);
            //Assert
            foreach (var item in result)
            {
                Assert.AreEqual(item.Authors.Find(a => a.Id == author.Id), author.Id);
            }
        }

    }
}