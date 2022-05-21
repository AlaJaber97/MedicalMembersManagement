using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Handler
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnauthorizedException()
        {
        }
    }
}
