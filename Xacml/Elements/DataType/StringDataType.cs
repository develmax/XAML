namespace Xacml.Elements.DataType
{
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class StringDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#string";
        public static readonly URI URIID = URI.Create(Identifer);
        private readonly string _value;

        public StringDataType(string value)
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
            Node child = node.FirstChild;

            if (child == null) return new StringDataType("");

            short type = child.NodeType;

            if ((type == Node.TEXT_NODE) || (type == Node.CDATA_SECTION_NODE) || (type == Node.COMMENT_NODE))
            {
                return GetInstance(child.NodeValue);
            }

            return null;
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new StringDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is StringDataType)
            {
                if (this == o) return true;
                if (this._value.Equals(((StringDataType)o).Value)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            hash = 37 * hash + (this._value != null ? this._value.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (StringDataType)t;
            return this.Value.CompareTo(obj.Value);
        }
    }
}