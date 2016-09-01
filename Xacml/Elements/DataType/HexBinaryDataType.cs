namespace Xacml.Elements.DataType
{
    using Xacml.Exceptions;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class HexBinaryDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#hexBinary";

        private const string HexDigits = "0123456789ABCDEFabcdef";
        public static readonly URI URIID = URI.Create(Identifer);
        private string _value;

        public HexBinaryDataType(string value)
            : base(URIID)
        {
            this._value = value.Trim();
            this.CheckHexBinary(this._value);
        }

        public override string Value
        {
            get { return this._value; }
        }

        private void CheckHexBinary(string value)
        {
            int len = value.Length;
            if ((len == 0) || ((len % 2) != 0))
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            for (int i = 0; i < len; i++)
            {
                if (HexDigits.IndexOf(value[i]) < 0)
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }
            }
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
            return new HexBinaryDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is HexBinaryDataType)
            {
                if (this == o)
                {
                    return true;
                }
                if (this._value.Equals(((HexBinaryDataType)o).Value))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            hash = 79 * hash + (this._value != null ? this._value.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (HexBinaryDataType)t;
            return this.Value.CompareTo(obj.Value);
        }
    }
}