namespace Xacml.Types.Streams
{
    using System.IO;

    using Xacml.Exceptions;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;

    public class InputStreamParser
    {
        public static Node Parse(InputStream input)
        {
            DocumentBuilderFactory dbFactory = DocumentBuilderFactory.NewInstance();

            dbFactory.IgnoringComments = true;
            dbFactory.IgnoringElementContentWhitespace = true;
            dbFactory.NamespaceAware = true;
            dbFactory.Validating = false;

            try
            {
                DocumentBuilder dber = dbFactory.NewDocumentBuilder();
                Document doc = dber.parse(input);

                string tagname = doc.ChildNodes.Item(0).NodeName;
                NodeList nodes = doc.getElementsByTagName(tagname);

                return nodes.Item(0);
            }
            catch (SAXException)
            {
                throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
            }
            catch (IOException)
            {
                throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
            }
            catch (ParserConfigurationException)
            {
                throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
            }
        }

        public static Node Parse(ByteArrayInputStream input)
        {
            return Parse(input.GetInputStream());
        }
    }
}