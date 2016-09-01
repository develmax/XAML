namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class RequestDefaults : IElement
    {
        public const string Identifer = "RequestDefaults";
        private readonly List<XPathVersion> _XPathVersions;

        public RequestDefaults(Node node)
        {
            if (node.NodeName.Equals(Identifer) == false)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            this._XPathVersions = new List<XPathVersion>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(XPathVersion.Identifer))
                {
                    this._XPathVersions.Add(new XPathVersion(child));
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
            psout.PrintLine(indenter + "<RequestDefaults>");
            foreach (XPathVersion xpath in this._XPathVersions)
            {
                xpath.Encode(output, indenter);
            }
            psout.PrintLine(indenter + "</RequestDefaults>");
        }

        #endregion
    }
}