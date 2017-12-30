using System;

namespace EWNServices.UserDefinedExceptions
{
    public class ProductPriceException : Exception
    {
        public ProductPriceException()
        { }

        public ProductPriceException(string message) : base(message)
        {
        }
    }
}
