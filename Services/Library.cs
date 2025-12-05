using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitalLibraryManager.Models;
using DigitalLibraryManager.Exceptions;

namespace DigitalLibraryManager.Services
{
    internal class Library
    {
        private List<Document> _documents = new List<Document>();

        public void AddDocument(Document doc)
        {
            _documents.Add(doc);
        }

        public void DisplayAll()
        {
            if (_documents.Count == 0)
            {
                Console.WriteLine("The library is empty.");
                return;
            }
            foreach (var doc in _documents)
            {
                doc.DisplayDetails();
            }
        }

        public void Search(string keyword)
        {
            var results = _documents.Where(d =>
                d.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                d.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                //error handeling later
                Console.WriteLine("No documents found matching that keyword.");
            }
            else
            {
                foreach (var doc in results) doc.DisplayDetails();
            }
        }
    }
}
