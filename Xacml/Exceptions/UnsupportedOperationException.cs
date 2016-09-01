namespace Xacml.Exceptions
{
    using System;

    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(string message)
            : base(message)
        {
        }
    }
}