namespace Xacml.Types.Xml
{
    using System.Xml;

    public class NodeList
    {
        private readonly XmlNodeList nodes;

        public NodeList(XmlNodeList childNodes)
        {
            this.nodes = childNodes;
        }

        public int Length
        {
            get { return this.nodes.Count; }
        }

        public Node Item(int i)
        {
            XmlNode tmp = this.nodes.Item(i);
            return tmp != null ? new Node(tmp) : null;
        }
    }
}