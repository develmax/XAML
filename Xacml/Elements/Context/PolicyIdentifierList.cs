namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Elements.Policy;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class PolicyIdentifierList : IElement
    {
        public const string Identifer = "PolicyIdentifierList";
        private readonly List<IElement> _referenceList;

        public PolicyIdentifierList(Node node)
        {
            this._referenceList = new List<IElement>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(PolicyIdReference.Identifer))
                {
                    this._referenceList.Add(PolicyIdReference.GetInstance(child));
                }
                else if (child.NodeName.Equals(PolicySetIdReference.Identifer))
                {
                    this._referenceList.Add(PolicySetIdReference.GetInstance(child));
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
            psout.PrintLine(indenter + "<PolicyIdentifierList>");
            indenter.Down();
            foreach (IElement element in this._referenceList)
            {
                element.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</PolicyIdentifierList>");
        }

        #endregion
    }
}