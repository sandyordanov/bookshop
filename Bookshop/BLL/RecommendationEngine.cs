using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RecommendationEngine
    {
        private List<Book> books;
        private List<User> users;

        public RecommendationEngine(List<Book> books, List<User> users)
        {
            this.books = books;
            this.users = users;
        }

        // Collaborative filtering recommendation method
        public List<Book> GetRecommendations(int userId)
        {
            // Find the user based on the provided userId
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            // Find other users who have rated similar books
            var similarUsers = FindSimilarUsers(user);

            // Aggregate ratings from similar users
            var aggregatedRatings = AggregateRatings(similarUsers);

            // Identify books that the user hasn't rated yet
            var unratedBooks = FindUnratedBooks(user);

            // Rank unrated books based on aggregated ratings
            var rankedBooks = RankBooks(aggregatedRatings, unratedBooks);

            // Return the top recommended books
            return rankedBooks;
        }

        private List<User> FindSimilarUsers(User targetUser)
        {
            return users.Where(u => u.Id != targetUser.Id &&
                                    u.Reviews.Select(rev => rev.Id).ToArray().Any(bookId => targetUser.Reviews.Select(rev => rev.Id).ToArray().Contains(bookId)))
                        .ToList();
        }

        private Dictionary<int, double> AggregateRatings(List<User> similarUsers)
        {
            // Aggregate ratings from similar users
            var aggregatedRatings = new Dictionary<int, double>();

            foreach (var user in similarUsers)
            {
                foreach (var bookId in user.Reviews.Select(rev => rev.Id).ToArray())
                {
                    if (!aggregatedRatings.ContainsKey(bookId))
                    {
                        aggregatedRatings[bookId] = 0;
                    }

                    // Weighted sum based on the similarity of users
                    aggregatedRatings[bookId] += 1.0 / similarUsers.Count;
                }
            }

            return aggregatedRatings;
        }

        private List<Book> FindUnratedBooks(User user)
        {
            // Find books that the user hasn't rated yet
            return books.Where(book => !user.Reviews.Select(rev => rev.Id).ToArray().Contains(book.Id)).ToList();
        }

        private List<Book> RankBooks(Dictionary<int, double> aggregatedRatings, List<Book> unratedBooks)
        {
            // Rank unrated books based on aggregated ratings
            return unratedBooks.OrderByDescending(book => aggregatedRatings.ContainsKey(book.Id) ? aggregatedRatings[book.Id] : 0)
                              .ToList();
        }
    }
}
