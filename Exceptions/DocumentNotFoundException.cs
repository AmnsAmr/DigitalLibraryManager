using System;

namespace DigitalLibraryManager.Exceptions
{
    public class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException(string message) : base(message)
        {
        }
    }
}