using System;

namespace Customer.TextParser
{
    public class DataParsingException : Exception
    {
        public DataParsingException()
        {
        }

        public DataParsingException(string message)
            : base(message)
        {
        }

        public DataParsingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}