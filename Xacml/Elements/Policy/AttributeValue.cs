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

    public class AttributeValue : IEvaluatable
    {
        internal const string stringIdentifer = "AttributeValue";
        private readonly string _anyAttributeName;
        private readonly string _anyAttributeValue;
        private readonly string _datatype;
        private readonly BagDataType _value;

        public AttributeValue(BagDataType bag)
        {
            this._value = new BagDataType();
            foreach (object o in bag.Children)
            {
                this._value.AddDataType((DataTypeValue)o);
                this._datatype = ((DataTypeValue)o).Identifier.ToString();
            }
        }

        public AttributeValue(Node node)
        {
            NamedNodeMap allattrs = node.Attributes;
            for (int i = 0; i < allattrs.Length; i++)
            {
                Node child = allattrs.Item(i);
                if (child.NodeName.Equals("DataType"))
                {
                    this._datatype = child.NodeValue;
                }
                else
                {
                    this._anyAttributeName = child.NodeName;
                    this._anyAttributeValue = child.NodeValue;
                }
            }
            if (this._datatype == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            this._value = new BagDataType();
            this._value.AddDataType(DataTypeFactory.Instance.CreateValue(this._datatype, node.TextContent));
        }

        public virtual string DataType
        {
            get { return this._datatype; }
        }

        public virtual DataTypeValue DataTypeValue
        {
            get { return (DataTypeValue)this._value.Children[0]; }
        }

        #region IEvaluatable Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.Print(indenter + "<AttributeValue DataType=\"" + this._datatype + "\"");
            if (this._anyAttributeName != null)
            {
                psout.Print(" " + this._anyAttributeName + "=\"" + this._anyAttributeValue + "\"");
            }
            psout.PrintLine(">");
            psout.PrintLine(indenter + this.DataTypeValue.Encode());
            psout.PrintLine((indenter + "</AttributeValue>"));
        }

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            return this._value;
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new AttributeValue(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AttributeValue(NodeFactory.GetInstanceFromString(value));
        }
    }
}