namespace Xacml.Elements.Policy
{
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class Description : IElement
    {
        public const string stringIdentifer = "Description";

        private readonly string _description;

        public Description(Node node)
        {
            this._description = node.TextContent.Trim();
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Description>");
            psout.PrintLine(indenter + this._description);
            psout.PrintLine(indenter + "</Description>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new Description(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Description(NodeFactory.GetInstanceFromString(value));
        }
    }
}