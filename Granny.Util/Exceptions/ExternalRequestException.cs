using System;

namespace Granny.Util.Exceptions
{
    public class ExternalRequestException : Exception
    {
        public ExternalRequestException(string message) : base(message)
        {

        }
    }
}
