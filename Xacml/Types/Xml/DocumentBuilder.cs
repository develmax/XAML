namespace Xacml.Types.Xml
{
    using System.Xml;

    using Xacml.Types.Streams;

    public class DocumentBuilder
    {
        public Document parse(InputStream input)
        {
            var xdoc = new XmlDocument();

            xdoc.Load(input.Stream);

            foreach (XmlNode childNode in xdoc.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.XmlDeclaration)
                {
                    xdoc.RemoveChild(childNode);
                    break;
                }
            }

            var doc = new Document(xdoc);
            return doc;
        }
    }
}