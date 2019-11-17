using System;
using System.Collections.Generic;
using System.Text;

namespace Granny.Util.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}
