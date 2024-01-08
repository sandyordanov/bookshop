using BLL;
using Classes;
using DAL;

namespace UnitTests
{
    [TestClass]
    public class RecommendationEngineTests
    {
        [TestMethod]
        public void FindSimilarUsers_Should_Find_Users_That_Have_Reviews_On_The_Same_Book()
        {
            // Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 50, "029382931", "12331242");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);


            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            user1.Reviews = new List<Review>() { new Review(1, "comment", 5, DateTime.Now, 0, user1, book1) };
            user2.Reviews = new List<Review>() { new Review(2, "comment", 4, DateTime.Now, 0, user2, book1) };

            users.Add(user1);
            users.Add(user2);
            RecommendationEngine engine = new RecommendationEngine(books, users);


            // Act
            var result = engine.FindSimilarUsers(user1);

            // Assert
            Assert.AreEqual(result[0], user2);
        }

        [TestMethod]
        public void FindSimilarUsers_Should_Return_An_Empty_List_When_There_Are_No_Users_That_Have_Reviews_On_The_Same_Book()
        {
            // Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 50, "029382931", "12331242");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);


            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            user1.Reviews = new List<Review>() { new Review(1, "comment", 5, DateTime.Now, 0, user1, book1) };
            user2.Reviews = new List<Review>() { new Review(2, "comment", 4, DateTime.Now, 0, user2, book2) };

            users.Add(user1);
            users.Add(user2);
            RecommendationEngine engine = new RecommendationEngine(books, users);


            // Act
            var result = engine.FindSimilarUsers(user1);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void AggregateRatings_Should_Return_A_Dictionary_Where_The_Sum_Of_Ratings_About_Books_Is_Equal_To_The_Expected()
        {
            //Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 50, "029382931", "12331242");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);


            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            user1.Reviews = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1),
                new Review(3, "comment", 1, DateTime.Now, 0, user1, book2) };
            user2.Reviews = new List<Review>() {
                new Review(2, "comment", 4, DateTime.Now, 0, user2, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book2) };

            users.Add(user1);
            users.Add(user2);
            RecommendationEngine engine = new RecommendationEngine(books, users);
            //Act
            var result = engine.AggregateRatings(users);
            //Assert
            Assert.AreEqual(9, result[1]);
            Assert.AreEqual(3, result[2]);
        }
        [TestMethod]
        public void FindUnratedBooks_Should_Return_List_Of_Books_That_Are_Not_Reviewed_By_The_User()
        {
            //Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 50, "029382931", "12331242");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 2.4, "downloadlink.com");
            var book3 = new EBook(3, "The Great Book3", "A classic romance novel.", "Publisher3", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre3" }) }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);
            books.Add(book3);

            var users = new List<User>();

            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            user1.Reviews = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1),
                new Review(2, "comment", 1, DateTime.Now, 0, user1, book3) };
            book1.AddReview(user1.Reviews[0]);
            book3.AddReview(user1.Reviews[1]);
            user2.Reviews = new List<Review>() {
                new Review(3, "comment", 4, DateTime.Now, 0, user2, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book2) };
            book1.AddReview(user2.Reviews[0]);
            book2.AddReview(user2.Reviews[1]);
            users.Add(user1);
            users.Add(user2);

            //Act
            RecommendationEngine engine = new RecommendationEngine(books, users);
            //Assert
            var result = engine.FindUnratedBooks(user1);
            Assert.IsTrue(result.Contains(book2));
        }
        [TestMethod]
        public void RankBooks_Should_Return_Ordered_Books_By_Their_Aggregated_Rating()
        {
            //Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 50, "029382931", "12331242");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 2.4, "downloadlink.com");
            var book3 = new EBook(3, "The Great Book3", "A classic romance novel.", "Publisher3", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre3" }) }, 2.4, "downloadlink.com");
            books.Add(book1);
            books.Add(book2);
            books.Add(book3);

            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            user1.Reviews = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1),
                new Review(3, "comment", 4, DateTime.Now, 0, user1, book2) };
            user2.Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user2, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book3) };

            users.Add(user1);
            users.Add(user2);
            RecommendationEngine engine = new RecommendationEngine(books, users);
            var aggregatedRatings = engine.AggregateRatings(users);
            //Act
            var result = engine.RankBooks(aggregatedRatings, books);
            //Assert
            Assert.AreEqual(result[0],book1);
            Assert.AreEqual(result[1],book2);
            Assert.AreEqual(result[2],book3);
        }
        [TestMethod]
        public void GetRecommendations_Should_Return_Proper_Recommendations_List()
        {
            //Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 50, "029382931", "12331242");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1", new List<string>() { "genre1", "genre2" }) }, 2.4, "downloadlink.com");
            books.Add(book1);
            books.Add(book2);

            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            user1.Reviews = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1) };
            user2.Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user2, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book2) };
            book1.AddReview(user1.Reviews[0]);
            book1.AddReview(user2.Reviews[0]);
            book2.AddReview(user2.Reviews[1]);
            users.Add(user1);
            users.Add(user2);
            RecommendationEngine engine = new RecommendationEngine(books, users);
            //Act
            var result = engine.GetRecommendations(user1.Id);
            //Assert
            Assert.IsTrue(result.Contains(book2));
        }
    }
}