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

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1234123412", "1234123412");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);


            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");

            users.Add(user1);
            users.Add(user2);

            Dictionary<User, List<Review>> usersReviews = new Dictionary<User, List<Review>>();
            Dictionary<Book, List<Review>> booksReviews = new Dictionary<Book, List<Review>>();
            usersReviews.Add(user1, new List<Review>() { new Review(1, "comment", 5, DateTime.Now, 0, user1, book1) });
            booksReviews.Add(book1, new List<Review>() { new Review(2, "comment", 4, DateTime.Now, 0, user2, book1) });
            RecommendationEngine engine = new RecommendationEngine(booksReviews, usersReviews);


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

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1234123412", "1234123411");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);


            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");

            users.Add(user1);
            users.Add(user2);

            Dictionary<User, List<Review>> usersReviews = new Dictionary<User, List<Review>>();
            Dictionary<Book, List<Review>> booksReviews = new Dictionary<Book, List<Review>>();
            usersReviews.Add(user1, new List<Review>() { new Review(1, "comment", 5, DateTime.Now, 0, user1, book1) });
            booksReviews.Add(book1, new List<Review>() { new Review(2, "comment", 4, DateTime.Now, 0, user2, book2) });
            RecommendationEngine engine = new RecommendationEngine(booksReviews, usersReviews);


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

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "1234123412", "1234123412");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);


            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            var userReviews1 = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1),
                new Review(3, "comment", 1, DateTime.Now, 0, user1, book1) };
            var userReviews2 = new List<Review>() {
                new Review(2, "comment", 4, DateTime.Now, 0, user2, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book2) };


            Dictionary<User, List<Review>> usersReviews = new Dictionary<User, List<Review>>();
            Dictionary<Book, List<Review>> booksReviews = new Dictionary<Book, List<Review>>();
            usersReviews.Add(user1, userReviews1);
            booksReviews.Add(book1, userReviews1);
            RecommendationEngine engine = new RecommendationEngine(booksReviews, usersReviews);
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

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "0293829311", "1233124211");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");
            var book3 = new EBook(3, "The Great Book3", "A classic romance novel.", "Publisher3", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");

            books.Add(book1);
            books.Add(book2);
            books.Add(book3);

            var users = new List<User>();

            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            var user1Reviews = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1),
                new Review(2, "comment", 1, DateTime.Now, 0, user1, book1) };

            var user2Reviews = new List<Review>() {
                new Review(3, "comment", 4, DateTime.Now, 0, user2, book2),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book2) };

            users.Add(user1);
            users.Add(user2);
            Dictionary<User, List<Review>> usersReviews = new Dictionary<User, List<Review>>();
            Dictionary<Book, List<Review>> booksReviews = new Dictionary<Book, List<Review>>();
            usersReviews.Add(user1, user1Reviews);
            booksReviews.Add(book1, user2Reviews);
            booksReviews.Add(book2, user2Reviews);
            //Act
            RecommendationEngine engine = new RecommendationEngine(booksReviews, usersReviews);
            //Assert
            var result = engine.FindUnratedBooks(user1);
            Assert.IsTrue(result.Contains(book2));
        }
        [TestMethod]
        public void RankBooks_Should_Return_Ordered_Books_By_Their_Aggregated_Rating()
        {
            //Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "0293829311", "1233124223");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");
            var book3 = new EBook(3, "The Great Book3", "A classic romance novel.", "Publisher3", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");
            books.Add(book1);
            books.Add(book2);
            books.Add(book3);

            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            var user1Reviews = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1),
                new Review(3, "comment", 4, DateTime.Now, 0, user1, book2) };
            var user2Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user2, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book3) };
            var book2Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user1, book2) };
            var book1Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user1, book1) };
            users.Add(user1);
            users.Add(user2);
            Dictionary<User, List<Review>> usersReviews = new Dictionary<User, List<Review>>();
            Dictionary<Book, List<Review>> booksReviews = new Dictionary<Book, List<Review>>();
            usersReviews.Add(user1, user1Reviews);
            usersReviews.Add(user2, user2Reviews);
            booksReviews.Add(book2, book2Reviews);
            booksReviews.Add(book1, book1Reviews);
            RecommendationEngine engine = new RecommendationEngine(booksReviews, usersReviews);
            var aggregatedRatings = engine.AggregateRatings(users);
            //Act
            var result = engine.RankBooks(aggregatedRatings, books);
            //Assert
            Assert.AreEqual(result[0], book1);
            Assert.AreEqual(result[1], book2);
            Assert.AreEqual(result[2], book3);
        }
        [TestMethod]
        public void GetRecommendations_Should_Return_Proper_Recommendations_List()
        {
            //Arrange
            var books = new List<Book>();

            var book1 = new PaperBook(1, "The Great Book1", "A classic phylosophical novel.", "Publisher1", "English", DateTime.Now, Format.PAPERBACK, new List<Author>() { new Author(1, "Robert Brown", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 50, "0293829311", "1233124212");
            var book2 = new EBook(2, "The Great Book2", "A classic romance novel.", "Publisher2", "English", DateTime.Now, Format.EBOOK, new List<Author>() { new Author(1, "Jeniffer White", DateTime.Today, "description", "website.com", "twitter.com/author1") }, 2.4, "downloadlink.com");
            books.Add(book1);
            books.Add(book2);

            var users = new List<User>();
            var user1 = new User(1, "Jeff", "mail.jeff@jeff", "jeffUser123", "password1", "picture.png1");
            var user2 = new User(2, "Chef", "mail.chef@chef", "chefUser123", "password2", "picture.png2");
            var user1Reviews = new List<Review>() {
                new Review(1, "comment", 5, DateTime.Now, 0, user1, book1) };
            var user2Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user2, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book2) };
            var book1Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user1, book1),
                new Review(4, "comment", 2, DateTime.Now, 0, user2, book1) };
            var book2Reviews = new List<Review>() {
                new Review(2, "comment", 5, DateTime.Now, 0, user2, book2)};
            users.Add(user1);
            users.Add(user2);
            Dictionary<User, List<Review>> usersReviews = new Dictionary<User, List<Review>>();
            Dictionary<Book, List<Review>> booksReviews = new Dictionary<Book, List<Review>>();
            usersReviews.Add(user1, user1Reviews);
            usersReviews.Add(user2, user2Reviews);
            booksReviews.Add(book1, book1Reviews);

            RecommendationEngine engine = new RecommendationEngine(booksReviews, usersReviews);
            //Act
            var result = engine.GetRecommendations(user1.Id);
            //Assert
            Assert.IsTrue(result.Contains(book2));
        }
    }
}