using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLMS.Core.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException()
        {
        }

        public ApplicationException(string message)
            : base(message)
        {
        }

        public ApplicationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
