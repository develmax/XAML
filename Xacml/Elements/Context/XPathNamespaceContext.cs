namespace Xacml.Elements.Context
{
    using System.Xml;

    public class XPathNamespaceContext
    {
        private readonly XmlNamespaceManager _manager;

        public XPathNamespaceContext(XmlNameTable nameTable)
        {
            this._manager = new XmlNamespaceManager(nameTable);
        }

        public XmlNamespaceManager Manager
        {
            get { return this._manager; }
        }

        public virtual void AddNameSpace(string prefix, string uri)
        {
            this._manager.AddNamespace(prefix, uri);
        }

        public string GetNamespaceURI(string prefix)
        {
            return this._manager.LookupNamespace(prefix);
        }

        public string GetPrefix(string byNamespace)
        {
            return this._manager.LookupPrefix(byNamespace);
        }
    }
}