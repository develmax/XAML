namespace Xacml.Elements.DataType
{
    using System;
    using System.Text;

    using Xacml.Exceptions;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class DayTimeDurationDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#dayTimeDuration";
        private static readonly URI URIID = URI.Create(Identifer);
        private readonly int _days;
        private readonly int _hours;
        private readonly int _mins;
        private readonly bool _negative;
        private readonly int _seconds;
        private readonly string _value;

        public DayTimeDurationDataType(string value)
            : base(URIID)
        {
            this._value = value.Trim();
            var str = new StringBuilder();
            this._negative = false;
            int ret = this.ParseP(this._value, 0, str);
            if (ret == 2)
            {
                this._negative = true;
            }

            int offset = ret;

            offset += this.ParseIntUntilToken(this._value, offset, 'D', str);
            if (str.Length > 0)
            {
                this._days = Convert.ToInt32(str.ToString());
            }
            else
            {
                this._days = 0;
            }

            this._hours = 0;
            this._mins = 0;
            this._seconds = 0;
            if (this._value[offset] == 'T')
            {
                offset += this.ParseIntUntilToken(this._value, offset, 'H', str);
                if (str.Length > 0)
                {
                    this._hours = Convert.ToInt32(str.ToString());
                }

                offset += this.ParseIntUntilToken(this._value, offset, 'M', str);
                if (str.Length > 0)
                {
                    this._mins = Convert.ToInt32(str.ToString());
                }

                offset += this.ParseIntUntilToken(this._value, offset, 'S', str);
                if (str.Length > 0)
                {
                    this._seconds = Convert.ToInt32(str.ToString());
                }
            }
        }

        public override string Value
        {
            get { return this._value; }
        }

        public virtual int Day
        {
            get { return this._negative ? -this._days : this._days; }
        }

        public virtual int Hour
        {
            get { return this._negative ? -this._hours : this._hours; }
        }

        public virtual int Minute
        {
            get { return this._negative ? -this._mins : this._mins; }
        }

        public virtual int Second
        {
            get { return this._negative ? -this._seconds : this._seconds; }
        }

        public virtual int Offset
        {
            get
            {
                int offset = this._days * 24 * 60 * 60;
                offset += this._hours * 60 * 60;
                offset += this._mins * 60;
                offset += this._seconds;
                return this._negative ? -offset : offset;
            }
        }

        private int ParseP(string value, int offset, StringBuilder result)
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
            if (this._days > 0)
            {
                str.Append(Convert.ToString(this._days));
                str.Append('D');
            }
            if (this._hours > 0)
            {
                str.Append(Convert.ToString(this._hours));
                str.Append('H');
            }
            if (this._mins > 0)
            {
                str.Append(Convert.ToString(this._mins));
                str.Append('M');
            }
            if (this._seconds > 0)
            {
                str.Append(Convert.ToString(this._seconds));
                str.Append('S');
            }

            return str.ToString();
        }

        public static DataTypeValue GetInstance(Node node)
        {
            return GetInstance(node.FirstChild.TextContent);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new DayTimeDurationDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is DateTimeDataType)
            {
                if (this == o)
                {
                    return true;
                }
                if (this._value.Equals(((DayTimeDurationDataType)o).Value))
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
            var obj = (DayTimeDurationDataType)t;
            if (this.Offset == obj.Offset)
            {
                return 0;
            }
            else if (this.Offset > obj.Offset)
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