namespace Xacml.Elements.DataType
{
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class AnyURIDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#anyURI";

        public static readonly URI URIID = URI.Create(Identifer);
        private readonly string _value;

        public AnyURIDataType(string value)
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
            return new AnyURIDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is AnyURIDataType)
            {
                if (this == o) return true;
                if (this._value.Equals(((AnyURIDataType)o).Value)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 89 * hash + (this._value != null ? this._value.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (AnyURIDataType)t;
            return this._value.CompareTo(obj.Value);
        }
    }
}