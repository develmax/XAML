namespace Xacml.Types.Xml
{
    using System.Xml;

    public class Document
    {
        private readonly XmlDocument doc;

        public Document(XmlDocument xdoc)
        {
            this.doc = xdoc;
        }

        public NodeList ChildNodes
        {
            get { return new NodeList(this.doc.ChildNodes); }
        }

        public NodeList getElementsByTagName(string tagname)
        {
            XmlNodeList items = this.doc.GetElementsByTagName(tagname);
            return new NodeList(items);
        }
    }
}