namespace Xacml.Exceptions
{
    using System;

    public class IllegalExpressionEvaluationException : Exception
    {
        public IllegalExpressionEvaluationException(string msg)
            : base(msg)
        {
        }
    }
}