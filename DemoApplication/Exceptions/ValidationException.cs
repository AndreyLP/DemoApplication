using System;

namespace DemoApplication.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
