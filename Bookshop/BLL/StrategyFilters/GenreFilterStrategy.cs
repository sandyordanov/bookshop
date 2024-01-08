using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyFilters
{
    public class GenreFilterStrategy : IBookFilterStrategy
    {
        private string genre;

        public GenreFilterStrategy(string genre)
        {
            this.genre = genre;
        }

        public IEnumerable<Book> Filter(IEnumerable<Book> books)
        {
            return books.Where(book => book.Genre == genre);
        }
    }
}
