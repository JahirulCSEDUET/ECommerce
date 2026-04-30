using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Business.Exceptions
{
    public class InvalidUserInputExceptions:Exception
    {
        public InvalidUserInputExceptions() { }
        public InvalidUserInputExceptions(string message) : base(message) { }
        public InvalidUserInputExceptions(string message, Exception InnerException) : base(message, InnerException) { }
    }
}
