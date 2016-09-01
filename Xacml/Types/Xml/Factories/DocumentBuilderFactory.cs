namespace Xacml.Types.Xml.Factories
{
    public class DocumentBuilderFactory
    {
        public bool IgnoringComments;
        public bool IgnoringElementContentWhitespace;
        public bool NamespaceAware;
        public bool Validating;

        public static DocumentBuilderFactory NewInstance()
        {
            return new DocumentBuilderFactory();
        }

        public DocumentBuilder NewDocumentBuilder()
        {
            return new DocumentBuilder();
        }
    }
}