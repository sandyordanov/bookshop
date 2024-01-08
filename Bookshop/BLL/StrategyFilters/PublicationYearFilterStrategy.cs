using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyFilters
{
    public class PublicationYearFilterStrategy : IBookFilterStrategy
    {
        private int publicationYear;

        public PublicationYearFilterStrategy(int publicationYear)
        {
            this.publicationYear = publicationYear;
        }

        public IEnumerable<Book> Filter(IEnumerable<Book> books)
        {
            return books.Where(book => book.PublicationDate.Year == publicationYear);
        }
    }
}
