namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class StatusCode : IElement
    {
        public const string Identifer = "StatusCode";

        public const string StatusCodeOk = "urn:oasis:names:tc:xacml:1.0:status:ok";
        public const string StatusCodeMissingAttribute = "urn:oasis:names:tc:xacml:1.0:status:missing-attribute";
        public const string StatusCodeSyntaxError = "urn:oasis:names:tc:xacml:1.0:status:syntax-error";
        public const string StatusCodeProcessingError = "urn:oasis:names:tc:xacml:1.0:status:processing-error";

        private readonly IList<StatusCode> _minorStatusCode;
        private readonly string _value;

        public StatusCode(string value)
        {
            this._value = value;
            this._minorStatusCode = null;
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine("<StatusCode Value=\"" + this._value + "\" ");
            indenter.Down();
            foreach (StatusCode s in this._minorStatusCode)
            {
                s.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine("</StatusCode");
        }

        #endregion
    }
}