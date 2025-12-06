using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibraryManager.Models
{
    internal abstract class Document
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public Document(string title, string author, int year)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            Year = year;
        }

        public Document(Guid id, string title, string author, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
        }

        public abstract void DisplayDetails();
        public abstract override string ToString();
    }
}
