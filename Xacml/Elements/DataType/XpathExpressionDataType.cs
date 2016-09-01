namespace Xacml.Elements.DataType
{
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class XpathExpressionDataType : DataTypeValue
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:3.0:data-type:xpathExpression";
        private static readonly URI URIID = URI.Create(Identifer);
        private readonly string _value;

        public XpathExpressionDataType(string value)
            : base(URIID)
        {
            this._value = value.Trim();
        }

        public override string Value
        {
            get { return this._value; }
        }

        public override string Encode()
        {
            return this._value;
        }

        public static DataTypeValue GetInstance(Node node)
        {
            return GetInstance(node.FirstChild.TextContent);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new XpathExpressionDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is XpathExpressionDataType)
            {
                if (this == o) return true;
                if (this._value.Equals(((XpathExpressionDataType)o).Value)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 31 * hash + (this._value != null ? this._value.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (XpathExpressionDataType)t;
            return this.Value.CompareTo(obj.Value);
        }
    }
}