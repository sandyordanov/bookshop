using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    internal class AuthorFilterStrategy : IBookFilterStrategy
    {
        private Author author;

        public AuthorFilterStrategy(Author author)
        {
            this.author = author;
        }

        public IEnumerable<Book> Filter(IEnumerable<Book> books)
        {
            return books.Where(book => book.Authors.Contains(author));
        }
    }
}
