namespace Xacml.Types.Streams
{
    using System.Text;

    public class PrintStream
    {
        private readonly OutputStream _output;

        public PrintStream(OutputStream output)
        {
            this._output = output;
        }

        public void PrintLine(string s)
        {
            this._output.Out.WriteLine(s);
        }

        public void PrintLine(StringBuilder buf)
        {
            this._output.Out.Write(buf.ToString());
        }

        public void Print(string s)
        {
            this._output.Out.Write(s);
        }

        public void PrintLine()
        {
            this._output.Out.WriteLine(string.Empty);
        }
    }
}