namespace Xacml.Elements.Policy
{
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class CombinerParameter : IElement
    {
        public const string stringIdentifer = "CombinerParameter";
        private readonly AttributeValue _attributeValue;
        private readonly string _parameterName;

        public CombinerParameter(Node node)
        {
            Node attr = node.Attributes.GetNamedItem("ParameterName");
            if (attr != null)
            {
                this._parameterName = attr.NodeValue;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            this._attributeValue = null;
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(AttributeValue.stringIdentifer))
                {
                    this._attributeValue = (AttributeValue)PolicyElementFactory.GetInstance(child);
                    break;
                }
            }
            if (this._attributeValue == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
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
            psout.PrintLine(indenter + "<CombinerParameter ParameterName=\"" + this._parameterName + "\" >");
            indenter.Down();
            this._attributeValue.Encode(output, indenter);
            indenter.Up();
            psout.PrintLine(indenter + "</CombinerParameter>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new CombinerParameter(node);
        }

        public static IElement GetInstance(string value)
        {
            return new CombinerParameter(NodeFactory.GetInstanceFromString(value));
        }
    }
}