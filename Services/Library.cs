using DigitalLibraryManager.Models;
using DigitalLibraryManager.Exceptions;
using System;
using System.IO;

namespace DigitalLibraryManager.Services
{
    internal class Library
    {
        private static List<Document> _documents = new List<Document>();

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

        static private List<Document> Find(string keyword)
        {
            try
            {
                List<Document> results = _documents.Where(d =>
               d.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
               d.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                if (results.Count == 0)
                {
                    throw new Exception("No documents found matching that keyword.");
                }
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Document>();
            }
        }

        public bool Search(string keyword)
        {
            try
            {
                var results = Library.Find(keyword);
                foreach (var doc in results) doc.DisplayDetails();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }      
        }

        public bool Remove(string keyword)
        {
            try
            {
                foreach (Document D in _documents)
                {
                    if (D.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || D.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        _documents.Remove(D);
                        return true;
                    }
                }
            }
            catch (DocumentNotFoundException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return false;
        }

        public void Add(Document doc) 
        {
            _documents.Add(doc);
        }


        public bool Save(string filePath)
        {
            foreach (Document doc in _documents)
            {
                try
                {
                    
                }
                catch (DocumentNotFoundException ex)
                { 
                    Console.WriteLine(ex.ToString());
                }
                finally 
                { 
                }
            }
        }

    }
}
