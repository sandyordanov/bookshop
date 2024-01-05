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
            this.books = books.FindAll(book => book.GetReviews().Count() > 0);
            this.users = users.FindAll(user => user.Reviews.Count() > 0);
        }

        // Collaborative filtering recommendation method
        public List<Book> GetRecommendations(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("You need to leave at least one review on a book, before getting recommendations.");
            }

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

        public List<User> FindSimilarUsers(User targetUser)
        {
            List<User> similarUsers = new List<User>();

            foreach (User user in users)
            {
                if (user.Id != targetUser.Id)
                {
                    bool hasCommonReview = false;

                    foreach (Review review in user.Reviews)
                    {
                        if (targetUser.Reviews.Any(targetReview => targetReview.BookId == review.BookId))
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
                foreach (var bookId in user.Reviews.Select(rev => rev.BookId).ToArray())
                {
                    if (!aggregatedRatings.ContainsKey(bookId))
                    {
                        aggregatedRatings.Add(bookId, 0);
                    }

                    // Weighted sum based on the similarity of users
                    aggregatedRatings[bookId] += user.Reviews.First(r => r.BookId == bookId).Rating;
                }
            }

            return aggregatedRatings;
        }

        public List<Book> FindUnratedBooks(User user)
        {
            List<Book> unratedBooks = new List<Book>();

            foreach (Book book in books)
            {
                bool isRated = false;

                foreach (Review review in user.Reviews)
                {
                    if (review.BookId == book.Id)
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
