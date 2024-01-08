using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyFilters
{
    public class BookFilterContext
    {
        private List<IBookFilterStrategy> strategies;

        public BookFilterContext(List<IBookFilterStrategy> strategies)
        {
            this.strategies = strategies;
        }

        public IEnumerable<Book> FilterBooks(IEnumerable<Book> books)
        {
            IEnumerable<Book> result = books;

            foreach (var strategy in strategies)
            {
                result = strategy.Filter(result);
            }

            return result;
        }
    }
}
