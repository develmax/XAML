namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class AttributeDesignator : IEvaluatable
    {
        public const string stringIdentifer = "AttributeDesignator";

        private readonly string _attributeId;
        private readonly string _category;
        private readonly string _dataType;
        private readonly string _issuer;
        private readonly string _mustBePresent;

        public AttributeDesignator(Node node)
        {
            this._category = this.GetNodeAttribute(node, "Category", true);
            this._attributeId = this.GetNodeAttribute(node, "AttributeId", true);
            this._dataType = this.GetNodeAttribute(node, "DataType", true);
            this._mustBePresent = this.GetNodeAttribute(node, "MustBePresent", true);
            this._issuer = this.GetNodeAttribute(node, "Issuer", false);
        }

        #region IEvaluatable Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<AttributeDesignator Category=\"" + this._category + "\"");
            psout.PrintLine(indenter + " AttributeId=\"" + this._attributeId + "\"");
            psout.PrintLine(indenter + " DataType=\"" + this._dataType + "\"");
            psout.PrintLine(indenter + " MustBePresent=\"" + this._mustBePresent + "\"");
            if (this._issuer != null)
            {
                psout.PrintLine(indenter + " Issuer=\"" + this._issuer + "\"");
            }
            psout.PrintLine(indenter + " />");
        }

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            BagDataType ret = ctx.Evaluate(
                this._category, this._attributeId, this._dataType, this._mustBePresent, this._issuer);
            if (ret.Empty && this._mustBePresent.EqualsIgnoreCase("true"))
            {
                throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
            }
            return ret;
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
            return new AttributeDesignator(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AttributeDesignator(NodeFactory.GetInstanceFromString(value));
        }
    }
}