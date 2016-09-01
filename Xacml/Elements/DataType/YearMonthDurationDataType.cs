namespace Xacml.Elements.DataType
{
    using System;
    using System.Text;

    using Xacml.Exceptions;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class YearMonthDurationDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#yearMonthDuration";
        private static readonly URI URIID = URI.Create(Identifer);
        private readonly int _month;
        private readonly bool _negative;
        private readonly string _value;
        private readonly int _year;

        public YearMonthDurationDataType(string value)
            : base(URIID)
        {
            this._value = value.Trim();
            var str = new StringBuilder();
            int ret = this.parseP(this._value, 0, str);
            if (ret == 2)
            {
                this._negative = true;
            }
            else
            {
                this._negative = false;
            }
            int offset = ret;
            this._year = 0;
            this._month = 0;

            ret = this.ParseIntUntilToken(this._value, offset, 'Y', str);
            if (ret > 0)
            {
                this._year = Convert.ToInt32(str.ToString());
            }
            offset += ret;

            ret = this.ParseIntUntilToken(this._value, offset, 'M', str);
            if (ret > 0)
            {
                this._month = Convert.ToInt32(str.ToString());
            }
        }

        public override string Value
        {
            get { return this.Encode(); }
        }

        public virtual int Year
        {
            get { return this._negative ? -this._year : this._year; }
        }

        public virtual int Month
        {
            get { return this._negative ? -this._month : this._month; }
        }

        public virtual int Offset
        {
            get
            {
                int offset = this._year * 12 + this._month;
                return this._negative ? -offset : offset;
            }
        }

        private int parseP(string value, int offset, StringBuilder result)
        {
            int len = value.Length;
            result.Remove(0, result.Length);
            if (offset < len)
            {
                char c = value[offset];
                if (c == 'P')
                {
                    return 1;
                }
                else if ((offset + 1) < len && c == '-' && value[offset + 1] == 'P')
                {
                    result.Append('-');
                    return 2;
                }
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        private int ParseIntUntilToken(string value, int offset, char token, StringBuilder result)
        {
            int len = value.Length, start = offset;
            result.Remove(0, result.Length);
            char ch = Convert.ToChar(0);
            if (start == len)
            {
                return 0;
            }
            for (; ch != token && offset < len; offset++)
            {
                ch = value[offset];
                if (char.IsDigit(ch))
                {
                    result.Append(ch);
                }
            }
            if (ch == token)
            {
                return offset - start;
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        public override string Encode()
        {
            var str = new StringBuilder();
            if (this._negative)
            {
                str.Append('-');
            }
            str.Append('P');
            if (this._year > 0)
            {
                str.Append(Convert.ToString(this._year));
                str.Append('Y');
            }
            if (this._month > 0)
            {
                str.Append(Convert.ToString(this._month));
                str.Append('M');
            }
            return str.ToString();
        }

        public static DataTypeValue GetInstance(Node node)
        {
            return GetInstance(node.FirstChild.TextContent);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new YearMonthDurationDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is YearMonthDurationDataType)
            {
                if (this == o)
                {
                    return true;
                }
                if (this._value.Equals(((YearMonthDurationDataType)o).Value))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            hash = 97 * hash + (this._value != null ? this._value.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (YearMonthDurationDataType)t;
            if (this.Offset > obj.Offset)
            {
                return 1;
            }
            else if (this.Offset < obj.Offset)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}