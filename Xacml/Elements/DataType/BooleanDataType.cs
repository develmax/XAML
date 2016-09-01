namespace Xacml.Elements.DataType
{
    using System;

    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class BooleanDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#boolean";
        public static readonly URI URIID = URI.Create(Identifer);
        public static BooleanDataType False = new BooleanDataType(false);
        public static BooleanDataType True = new BooleanDataType(true);
        private readonly bool _value;

        public BooleanDataType(bool value)
            : base(URIID)
        {
            this._value = value;
        }

        public BooleanDataType(string value)
            : base(URIID)
        {
            value = value.Trim();
            if (value.EqualsIgnoreCase("true"))
            {
                this._value = true;
            }
            else if (value.EqualsIgnoreCase("false"))
            {
                this._value = false;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        public override string Value
        {
            get { return Convert.ToString(this._value); }
        }

        public virtual bool Boolean
        {
            get { return this._value; }
        }

        public override string Encode()
        {
            return Convert.ToString(this._value);
        }

        public static DataTypeValue GetInstance(Node node)
        {
            Node child = node.FirstChild;
            return GetInstance(child.NodeValue);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new BooleanDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is BooleanDataType)
            {
                if (this == o)
                {
                    return true;
                }
                if (this._value == (((BooleanDataType)o).Boolean))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 79 * hash + (this._value ? 1 : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (BooleanDataType)t;
            if (this._value == obj.Boolean)
            {
                return 0;
            }
            else if (this._value && obj.Boolean == false)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}