using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibraryManager.Models
{
    internal class Book : Document
    {
        public int PageCount { get; set; }

        public Book(string title, string author, int year, int pageCount)
            : base(title, author, year)
        {
            PageCount = pageCount;
        }

        public Book(Guid id, string title, string author, int year, int pageCount)
            : base(id, title, author, year)
        {
            PageCount = pageCount;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"[BOOK] {Title} by {Author} ({Year}) - {PageCount} pages (ID: {Id})");
        }
    }
}
