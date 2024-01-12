using BLL;
using Classes;
using DAL;
using System.Runtime.Intrinsics.X86;
using UnitTests.Fakes;

namespace UnitTests
{
    [TestClass]
    public class ReviewManagerTests
    {
        IReviewRepository reviewRepo = new FakeReviewDbMediator();
        [TestMethod]
        public void GetReviewsPagination_Should_ReturnCorrectReviews()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var book = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1231231232", "1231231231");
            var result = manager.GetReviewsPagination(book, 0, 1);
            //Assert
            Assert.AreEqual(result.Count(), 1);
        }
        [TestMethod]
        public void GetReviewsPagination_Should_ReturnEmptyList()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var book = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1231231232", "1231231231");
            var result = manager.GetReviewsPagination(book, 1, 1);
            //Assert
            Assert.AreEqual(result.Count(), 0);

        }
        [TestMethod]
        public void GetTotalReviewsCount_Should_ReturnCorrectCount()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var result = manager.GetTotalReviewCountForBook(1);
            //Assert
            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void UserHasAlreadyReviewed_Should_ReturnTrue()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var result = manager.UserHasReviewsOnBook(1, 1);
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void UserHasAlreadyReviewed_Should_ReturnFalse()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var result = manager.UserHasReviewsOnBook(1, 2);
            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GetReviewsByBook_Should_ReturnCorrectReviews()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var book = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1231231232", "1231231231");
            
            var result = manager.GetAllReviewsByBook(book);
            //Assert
            Assert.AreEqual(result.Count(), 1);
        }

        [TestMethod]
        public void GetReviewOnBookByUser_Should_ReturnCorrectReview()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var book = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1231231232", "1231231231");
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var result = manager.GetReviewOnBookByUser(user1, book);
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllReviewsByUser_Should_ReturnCorrectReviews()
        {
            //Arrange
            ReviewManager manager = new ReviewManager(reviewRepo);
            //Act
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var result = manager.GetAllReviewsByUser(user1);
            //Assert
            Assert.AreEqual(result.Count(), 1);
        }
    }
}