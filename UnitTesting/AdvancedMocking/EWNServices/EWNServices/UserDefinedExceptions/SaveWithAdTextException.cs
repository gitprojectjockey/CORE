using System;

namespace EWNServices.UserDefinedExceptions
{
    class SaveWithAdTextException : Exception
    {
        public SaveWithAdTextException()
        {
        }

        public SaveWithAdTextException(string message) : base(message)
        { }
    }
}
