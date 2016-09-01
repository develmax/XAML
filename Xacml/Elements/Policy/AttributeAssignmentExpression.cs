namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class AttributeAssignmentExpression : IObligationsAndAdviceAvaluatable
    {
        public const string stringIdentifer = "AttributeAssignmentExpression";
        private readonly string _attributeId;
        private readonly string _category;
        private readonly IEvaluatable _evaluatable;
        private readonly string _issuer;

        public AttributeAssignmentExpression(Node node)
        {
            this._attributeId = this.GetNodeAttribute(node, "AttributeId", true);
            this._category = this.GetNodeAttribute(node, "Category", false);
            this._issuer = this.GetNodeAttribute(node, "Issuer", false);
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeType == Node.ELEMENT_NODE)
                {
                    this._evaluatable = (IEvaluatable)PolicyElementFactory.GetInstance(child);
                }
            }
            if (this._evaluatable == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IObligationsAndAdviceAvaluatable Members

        public IElement Evaluate(EvaluationContext ctx, string SchemeID, string effect)
        {
            DataTypeValue value = this._evaluatable.Evaluate(ctx, SchemeID);
            if (value is BagDataType == false)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            return new AttributeAssignment(
                this._attributeId, this._category, this._issuer, new AttributeValue((BagDataType)value));
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<AttributeAssignmentExpression AttributeId=\"" + this._attributeId + "\"");
            if (this._category != null)
            {
                psout.PrintLine(indenter + " Category=\"" + this._category + "\"");
            }
            if (this._issuer != null)
            {
                psout.PrintLine(indenter + " Issuer=\"" + this._issuer + "\"");
            }
            psout.PrintLine(indenter + ">");
            indenter.Down();
            this._evaluatable.Encode(output, indenter);
            indenter.Up();
            psout.PrintLine(indenter + "</AttributeAssignmentExpression>");
        }

        #endregion

        private string GetNodeAttribute(Node node, string tag, bool isRequired)
        {
            NamedNodeMap attrs = node.Attributes;
            Node attr = attrs.GetNamedItem(tag);
            if (attr != null)
            {
                return attr.NodeValue;
            }

            if (isRequired)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            else
            {
                return null;
            }
        }

        public static IElement GetInstance(Node node)
        {
            return new AttributeAssignmentExpression(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AttributeAssignmentExpression(NodeFactory.GetInstanceFromString(value));
        }
    }
}