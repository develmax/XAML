namespace Xacml.Types.Xml.Factories
{
    public class XPathFactory
    {
        public static XPathFactory NewInstance()
        {
            return new XPathFactory();
        }

        public XPath NewXPath()
        {
            return new XPath();
        }
    }
}