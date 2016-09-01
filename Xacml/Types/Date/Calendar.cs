namespace Xacml.Types.Date
{
    using System;

    public class Calendar
    {
        public static object YEAR = calType.Year;
        public static object MONTH = calType.Month;
        public static object DAY = calType.Day;

        public static object MILLISECOND = calType.Milliseconds;
        public static object HOUR = calType.Hour;
        public static object MINUTE = calType.Minute;
        public static object SECOND = calType.Second;
        public static object ZONE_OFFSET = calType.Zone_Offset;
        public static object DAY_OF_MONTH = calType.Day_of_month;
        public static object HOUR_OF_DAY = calType.Hour_of_day;
        public static object DST_OFFSET = calType.Dts_offset;
        private DateTime _date = DateTime.Now;
        private TimeZone _timeZone = new TimeZone(TimeZoneInfo.Local);

        private Calendar(TimeZone timeZone)
        {
            TimeSpan offset = timeZone.Value.BaseUtcOffset;
            this._date = DateTime.UtcNow.Add(offset);
            this._timeZone = timeZone;
        }

        public static Calendar Instance
        {
            get { return new Calendar(new TimeZone(TimeZoneInfo.Local)); }
        }

        public TimeZone TimeZone
        {
            get { return this._timeZone; }
        }

        public DateTime Time
        {
            get { return this._date; }
        }

        public void set(object obj, int value)
        {
            if (obj == (object)calType.Year)
                this._date = new DateTime(
                    value, this._date.Month, this._date.Day, this._date.Hour, this._date.Minute, this._date.Second);
            else if ((int)obj == (int)calType.Month)
                this._date = new DateTime(
                    this._date.Year, value, this._date.Day, this._date.Hour, this._date.Minute, this._date.Second);
            else if ((int)obj == (int)calType.Day)
                this._date = new DateTime(
                    this._date.Year,
                    this._date.Month,
                    value,
                    this._date.Hour,
                    this._date.Minute,
                    this._date.Second);
            else if ((int)obj == (int)calType.Day_of_month)
                this._date = new DateTime(
                    this._date.Year,
                    this._date.Month,
                    value,
                    this._date.Hour,
                    this._date.Minute,
                    this._date.Second);
            else if ((int)obj == (int)calType.Hour)
                this._date = new DateTime(
                    this._date.Year,
                    this._date.Month,
                    this._date.Day,
                    value,
                    this._date.Minute,
                    this._date.Second);
            else if ((int)obj == (int)calType.Hour_of_day)
                this._date = new DateTime(
                    this._date.Year,
                    this._date.Month,
                    this._date.Day,
                    value,
                    this._date.Minute,
                    this._date.Second);
            else if ((int)obj == (int)calType.Minute)
                this._date = new DateTime(
                    this._date.Year,
                    this._date.Month,
                    this._date.Day,
                    this._date.Hour,
                    value,
                    this._date.Second);
            else if ((int)obj == (int)calType.Second)
                this._date = new DateTime(
                    this._date.Year,
                    this._date.Month,
                    this._date.Day,
                    this._date.Hour,
                    this._date.Minute,
                    value);
        }

        public int CompareTo(Calendar calendar)
        {
            return this.Time.CompareTo(calendar.Time);
        }

        public static Calendar GetInstance(TimeZone timeZone)
        {
            return new Calendar(timeZone);
        }

        public int get(object obj)
        {
            if ((int)obj == (int)calType.Year) return this._date.Year;
            else if ((int)obj == (int)calType.Month) return this._date.Month;
            else if ((int)obj == (int)calType.Day) return this._date.Day;
            else if ((int)obj == (int)calType.Hour) return this._date.Hour;
            else if ((int)obj == (int)calType.Minute) return this._date.Minute;
            else if ((int)obj == (int)calType.Second) return this._date.Second;
            else if ((int)obj == (int)calType.Day_of_month) return this._date.Day;
            else if ((int)obj == (int)calType.Hour_of_day) return this._date.Hour;
            else if ((int)obj == (int)calType.Zone_Offset) return this._timeZone.Value.BaseUtcOffset.Hours;
            else if ((int)obj == (int)calType.Dts_offset) return 0;

            return 0;
        }

        public void set(int year, int month, int day, int hour, int minute, int second)
        {
            this._date = new DateTime(year, month, day, hour, minute, second);
        }

        public void add(object obj, int value)
        {
            if (obj == (object)calType.Year) this._date.AddYears(value);
            else if ((int)obj == (int)calType.Month) this._date.AddMonths(value);
            else if ((int)obj == (int)calType.Day) this._date.AddDays(value);
            else if ((int)obj == (int)calType.Hour) this._date.AddHours(value);
            else if ((int)obj == (int)calType.Minute) this._date.AddMinutes(value);
            else if ((int)obj == (int)calType.Second) this._date.AddSeconds(value);
        }

        public void SetUtc()
        {
            this._timeZone = new TimeZone(TimeZoneInfo.Utc);
            this._date = DateTime.UtcNow;
        }

        #region Nested type: calType

        private enum calType
        {
            Year,
            Month,
            Day,
            Milliseconds,
            Hour,
            Minute,
            Second,
            Zone_Offset,
            Day_of_month,
            Hour_of_day,
            Dts_offset,
            Time
        }

        #endregion
    }
}