namespace Xacml.Types.Date
{
    using System;

    public class TimeZone
    {
        private readonly TimeZoneInfo _timeZone;

        public TimeZone(TimeZoneInfo timeZone)
        {
            this._timeZone = timeZone;
        }

        public TimeZoneInfo Value
        {
            get { return this._timeZone; }
        }

        public bool inDaylightTime(DateTime time)
        {
            return this._timeZone.IsDaylightSavingTime(time);
        }

        public static TimeZone getTimeZone(string timezone)
        {
            if (timezone == "GMT") timezone = "UTC";
            return new TimeZone(TimeZoneInfo.FindSystemTimeZoneById(timezone));
        }
    }
}