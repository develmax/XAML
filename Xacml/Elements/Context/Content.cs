namespace Xacml.Elements.Context
{
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class Content : IElement
    {
        public const string Identifer = "Content";
        private readonly Node _node;
        private readonly XPathNamespaceContext _xPathNamespaceContext;

        public Content(Node node)
        {
            this._xPathNamespaceContext = new XPathNamespaceContext(node.Value.OwnerDocument.NameTable);
            this._node = node;
            this.GetNamespace(node);
        }

        public virtual XPathNamespaceContext XPathNamespaceContext
        {
            get { return this._xPathNamespaceContext; }
        }

        public virtual Node ContentRoot
        {
            get { return this._node; }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var @out = new PrintStream(output);
            this.EncodeChild(this._node, @out, indenter);
        }

        #endregion

        private void GetNamespace(Node node)
        {
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (!string.IsNullOrEmpty(child.Prefix) && !string.IsNullOrEmpty(child.NamespaceURI))
                {
                    this._xPathNamespaceContext.AddNameSpace(child.Prefix, child.NamespaceURI);
                }
                this.GetNamespace(child);
            }
        }

        private void EncodeChild(Node node, PrintStream psout, Indentation indenter)
        {
            psout.Print(indenter + "<" + node.NodeName);
            NamedNodeMap attrs = this._node.Attributes;
            for (int i = 0; i < attrs.Length; i++)
            {
                Node attr = attrs.Item(i);
                psout.Print(" " + attr.NodeName + "=\"" + attr.NodeValue + "\" ");
            }
            psout.Print("> ");
            psout.PrintLine();
            indenter.Down();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeType == Node.ELEMENT_NODE)
                {
                    this.EncodeChild(child, psout, indenter);
                }
                else
                {
                    string txt = child.TextContent.Trim();
                    if (txt.Length > 0)
                    {
                        psout.PrintLine(indenter + txt);
                    }
                }
            }
            indenter.Up();
            psout.PrintLine(indenter + "</" + node.NodeName + ">");
        }

        public static Content GetInstance(Node node)
        {
            return new Content(node);
        }
    }
}