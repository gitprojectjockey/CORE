using System;
using System.Collections.Generic;
using System.Text;

namespace EWNServices.UserDefinedExceptions
{
    public class SaveWithReceiptException : Exception
    {
        public SaveWithReceiptException()
        {
        }

        public SaveWithReceiptException(string message) : base(message)
        {
        }
    }
}
