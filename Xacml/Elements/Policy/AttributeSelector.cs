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

    public class AttributeSelector : IEvaluatable
    {
        public const string stringIdentifer = "AttributeSelector";
        private readonly string _category;
        private readonly string _contextSelectorId;
        private readonly string _dataType;
        private readonly string _mustBePresent;
        private readonly string _path;

        public AttributeSelector(Node node)
        {
            this._category = this.GetNodeAttribute(node, "Category", true);
            this._contextSelectorId = this.GetNodeAttribute(node, "ContextSelectorId", false);
            this._dataType = this.GetNodeAttribute(node, "DataType", true);
            this._mustBePresent = this.GetNodeAttribute(node, "MustBePresent", true);
            this._path = this.GetNodeAttribute(node, "Path", true);
        }

        #region IEvaluatable Members

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            var helper = new SelectorXPathHelper();
            var bag = new BagDataType();
            try
            {
                NodeList ret = helper.EvaluateXPath(
                    this._path.Trim(), ctx.ContentRoot, ctx.Request.Content.XPathNamespaceContext);
                for (int i = 0; i < ret.Length; i++)
                {
                    Node node = ret.Item(i);
                    if (node.TextContent != null)
                    {
                        bag.AddDataType(DataTypeFactory.Instance.CreateValue(this._dataType, node.TextContent));
                    }
                }
            }
            catch (XPathExpressionException)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            if (bag.Empty && this._mustBePresent.EqualsIgnoreCase("true"))
            {
                throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
            }

            return bag;
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<AttributeSelector Category=\"" + this._category + "\"");
            if (this._contextSelectorId != null)
            {
                psout.PrintLine(indenter + " ContextSelectorId=\"" + this._contextSelectorId + "\"");
            }
            psout.PrintLine(indenter + " DataType=\"" + this._dataType + "\"");
            psout.PrintLine(indenter + " MustBePresent=\"" + this._mustBePresent + "\"");
            psout.PrintLine(indenter + " Path=\"" + this._path + "\"");
            psout.PrintLine(indenter + " />");
        }

        #endregion

        private string GetNodeAttribute(Node node, string tag, bool isRequired)
        {
            Node attr = node.Attributes.GetNamedItem(tag);
            if (attr != null)
            {
                return attr.NodeValue.Trim();
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
            return new AttributeSelector(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AttributeSelector(NodeFactory.GetInstanceFromString(value));
        }
    }
}