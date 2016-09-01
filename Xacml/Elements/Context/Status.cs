namespace Xacml.Elements.Context
{
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Status : IElement
    {
        public const string Identifer = "Status";
        private readonly StatusCode _statusCode;
        private readonly StatusDetail _statusDetail;
        private readonly StatusMessage _statusMessage;

        public Status(string code)
        {
            this._statusCode = new StatusCode(code);
            this._statusMessage = null;
            this._statusDetail = null;
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Status>");
            indenter.Down();
            this._statusCode.Encode(output, indenter);
            if (this._statusMessage != null)
            {
                this._statusMessage.Encode(output, indenter);
            }
            if (this._statusDetail != null)
            {
                this._statusDetail.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Status>");
        }

        #endregion
    }
}