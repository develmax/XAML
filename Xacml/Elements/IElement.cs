namespace Xacml.Elements
{
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public interface IElement
    {
        URI Identifier { get; }

        void Encode(OutputStream output, Indentation indenter);
    }
}