namespace Xacml.Types.Streams
{
    using System.IO;

    public class FileInputStream
    {
        public Stream Stream;

        public FileInputStream(string propertiesFileName)
        {
            this.Stream = new FileStream(propertiesFileName, FileMode.Open);
        }
    }
}