namespace Xacml.Elements.Context
{
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class StatusMessage : IElement
    {
        public const string Identifer = "StatusMessage";
        private readonly string _msg;

        public StatusMessage(Node node)
        {
            this._msg = node.TextContent;
        }

        public StatusMessage(string msg)
        {
            this._msg = msg;
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<StatusMessage>" + this._msg + "</StatusMessage>");
        }

        #endregion
    }
}