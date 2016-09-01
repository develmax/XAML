namespace Xacml.Types.Xml.Factories
{
    using System;
    using System.IO;
    using System.Linq;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;

    public class NodeFactory
    {
        public static Node GetInstanceFromString(string source)
        {
            byte[] buf = source.Select(i => Convert.ToByte(i)).ToArray();

            return InputStreamParser.Parse(new ByteArrayInputStream(buf));
        }

        public static Node GetInstanceFromFile(string filename)
        {
            return GetInstanceFromURL("file:///" + filename);
        }

        public static Node GetInstanceFromURL(string url)
        {
            try
            {
                var _url = new URL(url);
                return InputStreamParser.Parse(_url.openConnection().InputStream);
            }
            catch (IOException)
            {
                throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
            }
        }
    }
}