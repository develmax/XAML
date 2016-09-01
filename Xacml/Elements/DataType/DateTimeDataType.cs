namespace Xacml.Elements.DataType
{
    using Xacml.Exceptions;
    using Xacml.Types.Date;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    /// <summary>
    /// Format: '-'? yyyy '-' mm '-' dd 'T' hh ':' mm ':' ss ('.' s+)? (zzzzzz)?
    /// </summary>
    public class DateTimeDataType : DataTypeValue
    {
        public const string Identifer = "http://www.w3.org/2001/XMLSchema#dateTime";
        public static readonly URI URIID = URI.Create(Identifer);
        private readonly Calendar _calendar;

        public DateTimeDataType(string value)
            : base(URIID)
        {
            this._calendar = ParseXMLDateTime.ParseDateTime(value.Trim(), true, true);
        }

        public DateTimeDataType(DateTimeDataType datetime)
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

        public DateTimeDataType()
            : base(URIID)
        {
            this._calendar = Calendar.Instance;
        }

        public override string Value
        {
            get { return this.Encode(); }
        }

        public virtual Calendar Calendar
        {
            get { return this._calendar; }
        }

        public override string Encode()
        {
            try
            {
                return ParseXMLDateTime.CreateDateTimeString(this._calendar, true, true);
            }
            catch (Indeterminate)
            {
                return "";
            }
        }

        public static DataTypeValue GetInstance(Node node)
        {
            return GetInstance(node.FirstChild.TextContent);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new DateTimeDataType(value);
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
                if (this._calendar.Equals(((DateTimeDataType)o).Calendar))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 5;
            hash = 83 * hash + (this._calendar != null ? this._calendar.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            var obj = (DateDataType)t;
            return this._calendar.CompareTo(obj.Calendar);
        }
    }
}