namespace Xacml.Exceptions
{
    using System;

    using Xacml.Elements.Context;

    public class Indeterminate : Exception
    {
        public const string IndeterminateMissingAttribute = StatusCode.StatusCodeMissingAttribute;
        public const string IndeterminateSyntaxError = StatusCode.StatusCodeSyntaxError;
        public const string IndeterminateProcessingError = StatusCode.StatusCodeProcessingError;

        public Indeterminate(string msg)
            : base(msg)
        {
        }
    }
}