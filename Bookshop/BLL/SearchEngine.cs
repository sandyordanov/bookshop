using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class SearchEngine
    {
        public List<Book> SearchForBooks(string search, List<Book> books)
        {
            List<string> keyWords = Regex.Split(search.ToLower(), @"[\s,\.]+").ToList();
            List<Book> AllBooks = books;
            List<Book> matchingBooks = new List<Book>();
            foreach (string keyWord in keyWords)
            {
                foreach (Book book in AllBooks)
                {
                    if (book.Title.ToLower().Contains(keyWord) || book.Authors.Any(a => a.FullName.ToLower().Contains(keyWord)))
                    {
                        if (!matchingBooks.Contains(book))
                        {
                            matchingBooks.Add(book);
                        }
                    }
                }
            }
            return matchingBooks;
        }
    }
}
