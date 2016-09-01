namespace Xacml.Elements.DataType
{
    using System.Text;

    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class RFC822NameDataType : DataTypeValue
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:1.0:data-type:rfc822Name";
        private static readonly URI URIID = URI.Create(Identifer);
        private readonly string _value;

        public RFC822NameDataType(string value)
            : base(URIID)
        {
            if (value.Contains("@"))
            {
                string[] parts = value.StringSplit("@", true);
                var rfc822name = new StringBuilder(parts[0]);
                rfc822name.Append("@");
                rfc822name.Append(parts[1].ToLower());
                this._value = rfc822name.ToString();
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
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

            return GetInstance(child.NodeValue);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new RFC822NameDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is RFC822NameDataType)
            {
                if (this == o)
                {
                    return true;
                }
                if (this._value.Equals(((RFC822NameDataType)o).Value))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 17 * hash + (this._value != null ? this._value.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (RFC822NameDataType)t;
            return this.Value.CompareTo(obj.Value);
        }
    }
}