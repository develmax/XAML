namespace Xacml.Elements.DataType
{
    using System;

    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class DoubleDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#double";
        private static readonly URI URIID = URI.Create(Identifer);
        private readonly double _value;

        public DoubleDataType(string value)
            : base(URIID)
        {
            this._value = Convert.ToDouble(value);
        }

        public override string Value
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public virtual double Double
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
            return new DoubleDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is DoubleDataType)
            {
                if (this == o) return true;
                if (this._value == (((DoubleDataType)o).Double)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            hash = 29 * hash + (this._value.doubleToLongBits() ^
                                ((int)((uint)this._value.doubleToLongBits() >> 32)));
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (DoubleDataType)t;
            if (this.Double > obj.Double)
            {
                return 1;
            }
            else if (this.Double < obj.Double)
            {
                return -1;
            }
            else return 0;
        }
    }
}