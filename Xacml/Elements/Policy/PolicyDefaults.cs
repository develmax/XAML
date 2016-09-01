namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class PolicyDefaults : IElement
    {
        public const string stringIdentifer = "PolicyDefaults";
        private readonly XPathVersion _xPathVersion;

        public PolicyDefaults(Node node)
        {
            this._xPathVersion = null;
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(XPathVersion.Identifer))
                {
                    this._xPathVersion = (XPathVersion)PolicyElementFactory.GetInstance(child);
                    break;
                }
            }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<PolicyDefaults>");
            indenter.Down();
            if (this._xPathVersion != null)
            {
                this._xPathVersion.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</PolicyDefaults>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new PolicyDefaults(node);
        }

        public static IElement GetInstance(string value)
        {
            return new PolicyDefaults(NodeFactory.GetInstanceFromString(value));
        }
    }
}