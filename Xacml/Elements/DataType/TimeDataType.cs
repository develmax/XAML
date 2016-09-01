namespace Xacml.Elements.DataType
{
    using Xacml.Types.Date;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class TimeDataType : DataTypeValue
    {
        public const string stringIdentifer = "http://www.w3.org/2001/XMLSchema#time";
        public static readonly URI URIID = URI.Create(stringIdentifer);
        private readonly string _value;
        private Calendar _calendar;

        public TimeDataType(string value)
            : base(URIID)
        {
            this._value = value.Trim();
            this._calendar = null;
            this._calendar = ParseXMLDateTime.ParseDateTime(value, false, true);
        }

        public TimeDataType()
            : base(URIID)
        {
            this._calendar = Calendar.Instance;
            this._calendar.SetUtc();
            /*_Calendar.set(Calendar.YEAR, 0);
			_Calendar.set(Calendar.MONTH, 0);
			_Calendar.set(Calendar.DAY, 0);*/
        }

        public override string Value
        {
            get { return this._value; }
        }

        public virtual Calendar Calendar
        {
            get { return this._calendar; }
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
            return new TimeDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is DateDataType)
            {
                if (this == o) return true;
                if (this._calendar.Equals(((TimeDataType)o).Calendar)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            hash = 29 * hash + (this._calendar != null ? this._calendar.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (DateDataType)t;
            return this._calendar.CompareTo(obj.Calendar);
        }
    }
}