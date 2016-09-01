namespace Xacml.Elements.Context
{
    using System.Text;

    using Xacml.Elements.Policy;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class AttributeAssignment : IElement
    {
        public const string Identifer = "AttributeAssignment";
        private readonly string _attributeId;
        private readonly string _category;
        private readonly string _issuer;
        private readonly AttributeValue _value;

        public AttributeAssignment(string attributeId, string category, string issuer, AttributeValue value)
        {
            this._attributeId = attributeId;
            this._category = category;
            this._issuer = issuer;
            this._value = value;
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            var buf = new StringBuilder();
            buf.Append(indenter.ToString());
            buf.Append("<AttributeAssignment AttributeId=\"");
            buf.Append(this._attributeId);
            buf.Append("\" ");
            if (this._category != null)
            {
                buf.Append("Category=\"");
                buf.Append(this._category);
                buf.Append("\" ");
            }
            if (this._issuer != null)
            {
                buf.Append("Issuer=\"");
                buf.Append(this._issuer);
                buf.Append("\" ");
            }
            buf.Append(">");
            psout.PrintLine(buf);
            indenter.Down();
            this._value.Encode(output, indenter);
            indenter.Up();
            psout.PrintLine(indenter + "</AttributeAssignment>");
        }

        #endregion
    }
}