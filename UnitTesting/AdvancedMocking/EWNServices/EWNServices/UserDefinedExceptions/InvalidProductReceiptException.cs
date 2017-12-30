using System;

namespace EWNServices.UserDefinedExceptions
{
    public class InvalidProductReceiptException : Exception
    {
        public InvalidProductReceiptException()
        {}

        public InvalidProductReceiptException(string message) : base(message)
        {
        }
    }
}
