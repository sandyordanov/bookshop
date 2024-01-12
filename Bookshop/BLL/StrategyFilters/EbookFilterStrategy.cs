using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyFilters
{
    public class EbookFilterStrategy : IBookFilterStrategy
    {

        public IEnumerable<Book> Filter(IEnumerable<Book> books)
        {
            return books.Where(book => book.GetType() == typeof(EBook));
        }
    }
}
