using DigitalLibraryManager.Models;
using DigitalLibraryManager.Services;
using System;
using System.IO;

namespace DigitalLibraryManager
{
    internal class Program
    {
        private static readonly string DataFilePath = "Data/library_backup.txt";

        static void Main(string[] args)
        {
            Library library = new Library();
            string choice = "";

            // Load existing data at startup
            library.Load(DataFilePath);

            while (choice != "7")
            {
                DisplayMenu();
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        AddDocument(library);
                        break;
                    case "2":
                        library.DisplayAll();
                        break;
                    case "3":
                        SearchDocument(library);
                        break;
                    case "4":
                        RemoveDocument(library);
                        break;
                    case "5":
                        library.Save(DataFilePath);
                        break;
                    case "6":
                        library.Load(@"C:\Users\amine\source\repos\DigitalLibraryManager\DigitalLibraryManager\Data\library_backup.txt");
                        break;
                    case "7":
                        Console.WriteLine("Saving library before exit...");
                        library.Save(DataFilePath);
                        Console.WriteLine("Program quitting. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("📚 Digital Library Manager 📚");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1. Add Document");
            Console.WriteLine("2. Display All Documents");
            Console.WriteLine("3. Search Document (by Title/Author)");
            Console.WriteLine("4. Remove Document (by Title/Author)");
            Console.WriteLine("5. Save Library (to backup file)");
            Console.WriteLine("6. Load from File");
            Console.WriteLine("7. Quit and Save");
            Console.WriteLine("------------------------------");
        }

        static void AddDocument(Library library)
        {
            Console.WriteLine("\n--- Add Document ---");
            Console.Write("Enter Document Type (Book, Magazine, PDF): ");
            string type = Console.ReadLine()?.Trim().ToUpper() ?? "";

            Console.Write("Title: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Author: ");
            string author = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Year: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year.");
                return;
            }

            Document? doc = null;

            try
            {
                switch (type)
                {
                    case "BOOK":
                        Console.Write("Page Count: ");
                        if (int.TryParse(Console.ReadLine(), out int pages))
                            doc = new Book(title, author, year, pages);
                        break;
                    case "MAGAZINE":
                        Console.Write("Issue Number: ");
                        if (int.TryParse(Console.ReadLine(), out int issue))
                            doc = new Magazine(title, author, year, issue);
                        break;
                    case "PDF":
                        Console.Write("Size in MB: ");
                        if (double.TryParse(Console.ReadLine(), out double size))
                            doc = new PDFDocument(title, author, year, size);
                        break;
                    default:
                        Console.WriteLine("Invalid document type.");
                        return;
                }

                if (doc != null)
                {
                    library.Add(doc);
                    Console.WriteLine("Document added successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the document: {ex.Message}");
            }
        }

        static void SearchDocument(Library library)
        {
            Console.WriteLine("\n--- Search Document ---");
            Console.Write("Enter keyword (Title or Author): ");
            string keyword = Console.ReadLine()?.Trim() ?? "";
            library.Search(keyword);
        }

        static void RemoveDocument(Library library)
        {
            Console.WriteLine("\n--- Remove Document ---");
            Console.Write("Enter keyword to search for document to remove (Title or Author): ");
            string keyword = Console.ReadLine()?.Trim() ?? "";
            library.Remove(keyword);
        }
    }
}