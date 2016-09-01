namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class MultiRequests : IElement
    {
        public const string Identifer = "MultiRequests";
        private readonly List<RequestReference> _requestReferences;

        public MultiRequests(Node node)
        {
            this._requestReferences = new List<RequestReference>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(RequestReference.Identifer))
                {
                    this._requestReferences.Add(RequestReference.GetInstance(child));
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
            psout.PrintLine(indenter + "<MultiRequests>");
            indenter.Down();
            foreach (RequestReference r in this._requestReferences)
            {
                r.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</MultiRequests>");
        }

        #endregion

        public static MultiRequests GetInstance(Node child)
        {
            return new MultiRequests(child);
        }
    }
}