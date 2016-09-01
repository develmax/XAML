namespace Xacml.Types.Xml
{
    using System.Xml;

    public class Node
    {
        public static short ELEMENT_NODE = (short)XmlNodeType.Element;
        public static short TEXT_NODE = (short)XmlNodeType.Text;
        public static short CDATA_SECTION_NODE = (short)XmlNodeType.CDATA;
        public static short COMMENT_NODE = (short)XmlNodeType.Comment;
        private readonly XmlNode _node;

        public Node(XmlNode node)
        {
            this._node = node;
        }

        public string TextContent
        {
            get { return this._node.InnerText; }
        }

        public XmlNode Value
        {
            get { return this._node; }
        }

        public Node FirstChild
        {
            get
            {
                return this._node.FirstChild != null
                           ? new Node(this._node.FirstChild)
                           : null;
            }
        }

        public string NodeName
        {
            get { return this._node.Name; }
        }

        public NamedNodeMap Attributes
        {
            get { return new NamedNodeMap(this._node.Attributes); }
        }

        public string NodeValue
        {
            get { return this._node.Value; }
        }

        public short NodeType
        {
            get { return (short)this._node.NodeType; }
        }

        public NodeList ChildNodes
        {
            get { return new NodeList(this._node.ChildNodes); }
        }

        public string Prefix
        {
            get { return this._node.Prefix; }
        }

        public string NamespaceURI
        {
            get { return this._node.NamespaceURI; }
        }

        public bool IsEqualNode(Node node)
        {
            return this._node.Equals(node._node);
        }
    }
}