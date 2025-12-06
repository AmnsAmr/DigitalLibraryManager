using DigitalLibraryManager.Models;
using DigitalLibraryManager.Exceptions;
using System;
using System.IO;

namespace DigitalLibraryManager.Services
{
    internal class Library
    {
        private List<Document> _documents = new List<Document>();

        public void Add(Document doc)
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

        private List<Document> Find(string keyword)
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

        public bool Search(string keyword)
        {
            try
            {
                var results = Find(keyword);
                if (results.Count == 0)
                {
                    throw new DocumentNotFoundException($"No documents found matching the keyword: '{keyword}'.");
                }
                foreach (var doc in results) doc.DisplayDetails();
                return true;
            }
            catch (DocumentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }      
        }

        public bool Remove(string keyword)
        {
            var matches = Find(keyword);
            if (matches.Count == 0)
            {
                Console.WriteLine($"No documents found to remove matching the keyword: '{keyword}'.");
                return false;
            }

            int removedCount = 0;
            Console.WriteLine($"Found {matches.Count} document(s) matching '{keyword}'.");
            Console.WriteLine("------------------------------------------");

            foreach (var doc in matches.ToList())
            {
                doc.DisplayDetails();
                Console.Write("Delete this document (Y)es, (N)o, (S)kip the rest? [Y/N/S]: ");
                string choice = Console.ReadLine()?.Trim().ToUpper() ?? "";

                if (choice == "Y" || choice == "y")
                {
                    _documents.Remove(doc);
                    removedCount++;
                    Console.WriteLine("Document deleted.");
                }
                else if (choice == "S" || choice == "s")
                {
                    Console.WriteLine("Skipping the rest of the removal process.");
                    break;
                }
                else if (choice == "N" || choice == "n")
                {
                    Console.WriteLine("Skipping document.");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Skipping document.");
                }
                Console.WriteLine("------------------------------------------");
            }

            if (removedCount > 0)
            {
                Console.WriteLine($"Removal complete. {removedCount} document(s) were removed.");
                return true;
            }
            else
            {
                Console.WriteLine("No documents were removed.");
                return false;
            }
        }

        public bool Save(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    foreach (Document doc in _documents)
                    {
                        writer.WriteLine(doc.ToString());
                    }
                    Console.WriteLine($"Library successfully saved to {filePath}.");
                    return true;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error saving library to file: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during save: {ex.Message}");
                return false;
            }
        }
        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Backup file not found at: {filePath}. Starting with an empty library.");
                return;
            }

            try
            {
                _documents.Clear();

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        string type = parts[0];
                        switch (type)
                        {
                            case "Book":
                                _documents.Add(new Book(Guid.Parse(parts[1]),
                                    parts[2],
                                    parts[3],
                                    int.Parse(parts[4]),
                                    int.Parse(parts[5])));
                                break;
                            case "Magazine":
                                _documents.Add(new Magazine(Guid.Parse(parts[1]),
                                    parts[2],
                                    parts[3],
                                    int.Parse(parts[4]),
                                    int.Parse(parts[5])
                                    ));
                                break;
                            case "PDF":
                                _documents.Add(new PDFDocument(Guid.Parse(parts[1]),
                                    parts[2],
                                    parts[3],
                                    int.Parse(parts[4]),
                                    double.Parse(parts[5])
                                    ));
                                break;
                            default:
                                Console.WriteLine($"Unknown document type found in file: {type}");
                                break;
                        }
                    }
                    Console.WriteLine($"Library successfully loaded from {filePath}.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error loading library from file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing library data from file: {ex.Message}. Data may be corrupted.");
            }
        }

    }
}
