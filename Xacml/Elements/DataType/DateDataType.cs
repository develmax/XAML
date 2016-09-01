namespace Xacml.Elements.DataType
{
    using Xacml.Exceptions;
    using Xacml.Types.Date;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class DateDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#date";
        public static readonly URI URIID = URI.Create(Identifer);

        private readonly Calendar _calendar;
        private readonly string _date;

        public DateDataType(string value)
            : base(URIID)
        {
            this._date = value.Trim();
            this._calendar = ParseXMLDateTime.ParseDateTime(value, true, false);
        }

        public DateDataType(DateDataType datetime)
            : base(URIID)
        {
            Calendar cal = datetime.Calendar;
            this._calendar = Calendar.GetInstance(cal.TimeZone);
            this._calendar.set(
                cal.get(Calendar.YEAR),
                cal.get(Calendar.MONTH),
                cal.get(Calendar.DAY),
                cal.get(Calendar.HOUR),
                cal.get(Calendar.MINUTE),
                cal.get(Calendar.SECOND));
            this._calendar.set(Calendar.MILLISECOND, cal.get(Calendar.MILLISECOND));
        }

        public DateDataType()
            : base(URIID)
        {
            this._calendar = Calendar.Instance;
            this._calendar.set(Calendar.MILLISECOND, 0);
            this._calendar.set(Calendar.HOUR, 0);
            this._calendar.set(Calendar.MINUTE, 0);
            this._calendar.set(Calendar.SECOND, 0);
            this._calendar.set(Calendar.ZONE_OFFSET, 0);
        }

        public override string Value
        {
            get { return this._date; }
        }

        public virtual Calendar Calendar
        {
            get { return this._calendar; }
        }

        public override string Encode()
        {
            try
            {
                return ParseXMLDateTime.CreateDateTimeString(this._calendar, true, false);
            }
            catch (Indeterminate)
            {
                return "";
            }
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is DateDataType)
            {
                if (this == o) return true;
                if (this._calendar.Equals(((DateDataType)o).Calendar)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 5;
            hash = 97 * hash + (this._calendar != null ? this._calendar.GetHashCode() : 0);
            return hash;
        }

        public static DataTypeValue GetInstance(Node node)
        {
            return GetInstance(node.FirstChild.TextContent);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new DateDataType(value);
        }

        public override int CompareTo(object t)
        {
            var obj = (DateDataType)t;
            return this._calendar.CompareTo(obj.Calendar);
        }
    }
}