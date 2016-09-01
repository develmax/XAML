namespace Xacml.Types.Streams
{
    using System.IO;

    public class InputStream
    {
        public Stream Stream;

        public InputStream(Stream stream)
        {
            this.Stream = stream;
        }
    }
}