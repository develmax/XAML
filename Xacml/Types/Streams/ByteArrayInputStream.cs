namespace Xacml.Types.Streams
{
    using System.IO;

    public class ByteArrayInputStream
    {
        private readonly Stream _stream;

        public ByteArrayInputStream(byte[] source)
        {
            this._stream = new MemoryStream(source);
        }

        public InputStream GetInputStream()
        {
            return new InputStream(this._stream);
        }
    }
}