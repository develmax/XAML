namespace Xacml.Elements.Context
{
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class AttributesReference : IElement
    {
        public const string Identifer = "AttributesReference";

        private readonly string _referenceId;

        public AttributesReference(Node node)
        {
            NamedNodeMap attrs = node.Attributes;
            for (int i = 0; i < attrs.Length; i++)
            {
                Node child = attrs.Item(i);
                if (child.NodeName.Equals("ReferenceId"))
                {
                    this._referenceId = node.Attributes.GetNamedItem("ReferenceId").NodeValue.Trim();
                }
            }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var @out = new PrintStream(output);
            @out.PrintLine(indenter + "<AttributesReference ReferenceId=" + "\"" + this._referenceId + "\" />");
        }

        #endregion

        public static AttributesReference GetInstance(Node child)
        {
            return new AttributesReference(child);
        }
    }
}