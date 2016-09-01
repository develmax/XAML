namespace Xacml.Elements.Policy
{
    using System.Collections.Generic;

    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class PolicyIssuer : IElement
    {
        public const string stringIdentifer = "PolicyIssuer";
        private readonly List<Attribute> _attributes;
        private readonly Content _content;

        public PolicyIssuer(Node node)
        {
            NodeList children = node.ChildNodes;
            this._attributes = new List<Attribute>();
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(Content.Identifer))
                {
                    this._content = new Content(child);
                }
                else if (child.NodeName.Equals(Attribute.Identifer))
                {
                    this._attributes.Add(new Attribute(child));
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
            var @out = new PrintStream(output);
            @out.PrintLine(indenter + "<PolicyIssuer>");
            indenter.Down();
            if (this._content != null)
            {
                this._content.Encode(output, indenter);
            }

            foreach (Attribute a in this._attributes)
            {
                a.Encode(output, indenter);
            }
            indenter.Up();
            @out.PrintLine(indenter + "</PolicyIssuer>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new PolicyIssuer(node);
        }

        public static IElement GetInstance(string value)
        {
            return new PolicyIssuer(NodeFactory.GetInstanceFromString(value));
        }
    }
}