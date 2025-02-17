﻿using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyFilters
{
    public interface IBookFilterStrategy
    {
        IEnumerable<Book> Filter(IEnumerable<Book> books);
    }
}
