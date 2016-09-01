namespace Xacml.Elements.DataType
{
    using Xacml.Exceptions;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class Base64BinaryDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#base64Binary";

        private const string Base4 = "AQgw";
        private const string Base16 = "AEIMQUYcgkosw048";
        private const string Base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        public static readonly URI URIID = URI.Create(Identifer);

        private readonly string _value;

        public Base64BinaryDataType(string value)
            : base(URIID)
        {
            //remove all spaces and trim
            this._value = value.Replace("\\s+", "");
            this.CheckBase64Binary(this._value);
        }

        public override string Value
        {
            get { return this._value; }
        }

        private void TestCharInBase4(char test)
        {
            if (Base4.IndexOf(test) < 0)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        private void TestCharInBase16(char test)
        {
            if (Base16.IndexOf(test) < 0)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        private void TestCharInBase64(char test)
        {
            if (Base64.IndexOf(test) < 0)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        private void CheckBase64Binary(string value)
        {
            int len = value.Length;
            if (len == 0 || len % 4 != 0)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            for (int i = 0; i < (len - 4); i++)
            {
                this.TestCharInBase64(value[i]);
            }

            if (value[len - 1] == '=' && value[len - 2] == '=')
            {
                this.TestCharInBase4(value[len - 3]);
            }
            else if (value[len - 1] == '=')
            {
                this.TestCharInBase16(value[len - 2]);
                this.TestCharInBase64(value[len - 3]);
            }
            else
            {
                this.TestCharInBase64(value[len - 1]);
                this.TestCharInBase64(value[len - 2]);
                this.TestCharInBase64(value[len - 3]);
            }
            this.TestCharInBase64(value[len - 4]);
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
            return new Base64BinaryDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is Base64BinaryDataType)
            {
                if (this == o)
                {
                    return true;
                }
                if (this._value.Equals(((Base64BinaryDataType)o).Value))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 5;
            hash = 41 * hash + (this._value != null ? this._value.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (Base64BinaryDataType)t;
            return this._value.CompareTo(obj.Value);
        }
    }
}