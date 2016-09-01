namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class RequestReference : IElement
    {
        public const string Identifer = "RequestReference";
        private readonly List<AttributesReference> _AttributesReferences;

        public RequestReference(Node node)
        {
            this._AttributesReferences = new List<AttributesReference>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(AttributesReference.Identifer))
                {
                    this._AttributesReferences.Add(AttributesReference.GetInstance(child));
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
            psout.PrintLine(indenter + "<RequestReference>");
            indenter.Down();
            foreach (AttributesReference a in this._AttributesReferences)
            {
                a.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</RequestReference>");
        }

        #endregion

        public static RequestReference GetInstance(Node child)
        {
            return new RequestReference(child);
        }
    }
}