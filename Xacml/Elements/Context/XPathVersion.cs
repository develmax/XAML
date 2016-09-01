namespace Xacml.Elements.Context
{
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class XPathVersion : IElement
    {
        public const string Identifer = "XPathVersion";
        public const string XPathVersion10 = "http://www.w3.org/TR/1999/REC-xpath-19991116";
        public const string XPathVersion20 = "http://www.w3.org/TR/2007/REC-xpath20-20070123";
        public static readonly AnyURIDataType XPathVersion10AnyURI = new AnyURIDataType(XPathVersion10);
        public static readonly AnyURIDataType XPathVersion20AnyURI = new AnyURIDataType(XPathVersion20);
        private readonly AnyURIDataType _xPathVersion;

        public XPathVersion(Node node)
        {
            string txt = node.TextContent.Trim();
            if (txt.Equals(XPathVersion10))
            {
                this._xPathVersion = XPathVersion10AnyURI;
            }
            else if (txt.Equals(XPathVersion20))
            {
                this._xPathVersion = XPathVersion20AnyURI;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        public XPathVersion(string value)
        {
            if (value.Equals(XPathVersion10))
            {
                this._xPathVersion = XPathVersion10AnyURI;
            }
            else if (value.Equals(XPathVersion20))
            {
                this._xPathVersion = XPathVersion20AnyURI;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<XPathVersion>" + this._xPathVersion.Encode() + "</XPathVersion>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new XPathVersion(node);
        }

        public static IElement GetInstance(string value)
        {
            return new XPathVersion(value);
        }
    }
}