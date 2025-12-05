using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibraryManager.Models
{
    internal class PDFDocument : Document
    {
        public double SizeInMB { get; set; }

        public PDFDocument(string title, string author, int year, double size)
            : base(title, author, year)
        {
            SizeInMB = size;
        }

        public PDFDocument(Guid id, string title, string author, int year, double size)
            : base(id, title, author, year)
        {
            SizeInMB = size;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"[PDF] {Title} by {Author} ({Year}) - {SizeInMB} MB (ID: {Id})");
        }
    }
}
