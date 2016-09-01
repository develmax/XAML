namespace Xacml.Types.Streams
{
    using System.IO;

    public class OutputStream
    {
        private readonly TextWriter _out;

        public OutputStream(TextWriter output)
        {
            this._out = output;
        }

        public TextWriter Out
        {
            get { return this._out; }
        }
    }
}