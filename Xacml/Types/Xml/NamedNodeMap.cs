namespace Xacml.Types.Xml
{
    using System.Xml;

    public class NamedNodeMap
    {
        private readonly XmlNamedNodeMap _items;

        public NamedNodeMap(XmlNamedNodeMap attributes)
        {
            this._items = attributes;
        }

        public int Length
        {
            get { return this._items.Count; }
        }

        public Node Item(int i)
        {
            XmlNode tmp = this._items.Item(i);
            return tmp != null ? new Node(tmp) : null;
        }

        public Node GetNamedItem(string referenceid)
        {
            XmlNode tmp = this._items.GetNamedItem(referenceid);
            return tmp != null ? new Node(tmp) : null;
        }
    }
}