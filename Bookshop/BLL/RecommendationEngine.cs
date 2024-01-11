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
        private Dictionary<Book, List<Review>> books = new Dictionary<Book, List<Review>>();
        private Dictionary<User, List<Review>> users = new Dictionary<User, List<Review>>();

        public RecommendationEngine(Dictionary<Book,List<Review>> books, Dictionary<User,List<Review>> users)
        {
            foreach (var book in books)
            {
                if (book.Value.Count>0)
                {
                    this.books.Add(book.Key, book.Value);
                }
            }
            foreach (var user in users)
            {
                if (user.Value.Count > 0)
                {
                    this.users.Add(user.Key, user.Value);
                }
            }
        }

        // Collaborative filtering recommendation method
        public List<Book> GetRecommendations(int userId)
        {
            var user = users.FirstOrDefault(u => u.Key.Id == userId);
            if (user.Key == null)
            {
                throw new ArgumentException("You need to leave at least one review on a book, before getting recommendations.");
            }

            var similarUsers = FindSimilarUsers(user.Key);

            // Aggregate ratings from similar users
            var aggregatedRatings = AggregateRatings(similarUsers);

            // Identify books that the user hasn't rated yet
            var unratedBooks = FindUnratedBooks(user.Key);

            // Rank unrated books based on aggregated ratings
            var rankedBooks = RankBooks(aggregatedRatings, unratedBooks);

            // Return the top recommended books
            return rankedBooks;
        }

        public List<User> FindSimilarUsers(User targetUser)
        {
            List<User> similarUsers = new List<User>();

            foreach (User user in users.Keys)
            {
                if (user.Id != targetUser.Id)
                {
                    bool hasCommonReview = false;

                    foreach (Review review in users[user])
                    {
                        if (users[targetUser].Any(targetReview => targetReview.Book.Id == review.Book.Id))
                        {
                            hasCommonReview = true;
                            break;
                        }
                    }

                    if (hasCommonReview)
                    {
                        similarUsers.Add(user);
                    }
                }
            }

            return similarUsers;
        }


        public Dictionary<int, double> AggregateRatings(List<User> similarUsers)
        {
            // Aggregate ratings from similar users
            var aggregatedRatings = new Dictionary<int, double>();

            foreach (var user in similarUsers)
            {
                foreach (var bookId in users[user].Select(rev => rev.Book.Id).ToArray())
                {
                    if (!aggregatedRatings.ContainsKey(bookId))
                    {
                        aggregatedRatings.Add(bookId, 0);
                    }

                    // Weighted sum based on the similarity of users
                    aggregatedRatings[bookId] += users[user].First(r => r.Book.Id == bookId).Rating;
                }
            }

            return aggregatedRatings;
        }

        public List<Book> FindUnratedBooks(User user)
        {
            List<Book> unratedBooks = new List<Book>();

            foreach (Book book in books.Keys)
            {
                bool isRated = false;

                foreach (Review review in users[user])
                {
                    if (review.Book.Id == book.Id)
                    {
                        isRated = true;
                        break;
                    }
                }

                if (!isRated)
                {
                    unratedBooks.Add(book);
                }
            }

            return unratedBooks;
        }


        public List<Book> RankBooks(Dictionary<int, double> aggregatedRatings, List<Book> unratedBooks)
        {
            return unratedBooks.OrderByDescending(delegate (Book book)
            {
                if (aggregatedRatings.ContainsKey(book.Id))
                {
                    return aggregatedRatings[book.Id];
                }
                else
                {
                    return 0;
                }
            }).ToList();
        }
    }
}
