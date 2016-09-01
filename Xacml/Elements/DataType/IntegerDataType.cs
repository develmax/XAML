namespace Xacml.Elements.DataType
{
    using System;

    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class IntegerDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#integer";
        private static readonly URI URIID = URI.Create(Identifer);
        private readonly int _value;

        public IntegerDataType(string value)
            : base(URIID)
        {
            this._value = Convert.ToInt32(value);
        }

        public override string Value
        {
            get { return Convert.ToString(this._value); }
        }

        public virtual int Integer
        {
            get { return this._value; }
        }

        public override string Encode()
        {
            return Convert.ToString(this._value);
        }

        public static DataTypeValue GetInstance(Node node)
        {
            return GetInstance(node.FirstChild.TextContent);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new IntegerDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is IntegerDataType)
            {
                if (this == o) return true;
                if (this._value == (((IntegerDataType)o).Integer)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 5;
            hash = 11 * hash + this._value;
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (IntegerDataType)t;
            if (this.Integer > obj.Integer)
            {
                return 1;
            }
            else if (this.Integer < obj.Integer)
            {
                return -1;
            }
            else return 0;
        }
    }
}