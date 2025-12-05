using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibraryManager.Models
{
    internal class Magazine : Document
    {
        public int IssueNumber { get; set; }

        public Magazine(string title, string author, int year, int issueNumber)
            : base(title, author, year)
        {
            IssueNumber = issueNumber;
        }

        public Magazine(Guid id, string title, string author, int year, int issueNumber)
            : base(id, title, author, year)
        {
            IssueNumber = issueNumber;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"[MAGAZINE] {Title} by {Author} ({Year}) - Issue N°{IssueNumber} (ID: {Id})");
        }
    }
}
