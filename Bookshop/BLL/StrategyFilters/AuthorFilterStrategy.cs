using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyFilters
{
    public class AuthorFilterStrategy : IBookFilterStrategy
    {
        private Author author;

        public AuthorFilterStrategy(Author author)
        {
            this.author = author;
        }

        public IEnumerable<Book> Filter(IEnumerable<Book> books)
        {
            foreach (Book book in books)
            {
                   foreach (Author author in book.Authors)
                {
                    if (author.Id == this.author.Id)
                    {
                        yield return book;
                    }
                }
            }
        }
    }
}
