namespace Xacml.Elements.DataType
{
    using System;
    using System.Text;

    using Xacml.Exceptions;
    using Xacml.Types.Date;

    using TimeZone = Xacml.Types.Date.TimeZone;

    public class ParseXMLDateTime
    {
        private static int ParseInt(string parsedstring, int offset, int count)
        {
            int len = parsedstring.Length;
            var digits = new StringBuilder();
            if (offset + count <= len)
            {
                for (int i = offset; i < (offset + count); i++)
                {
                    char c = parsedstring[i];
                    if (char.IsDigit(c))
                    {
                        digits.Append(c);
                    }
                }

                if (digits.Length == count)
                {
                    return Convert.ToInt32(digits.ToString());
                }
            }

            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        private static int SkipOneCharacter(string parsedstring, int offset, char c, bool optional)
        {
            if (offset < parsedstring.Length)
            {
                if (parsedstring[offset] == c)
                {
                    return 1;
                }
                else if (optional)
                {
                    return 0;
                }
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        private static string ParseTimeZone(string parsedstring, int offset)
        {
            var timezone = new StringBuilder("GMT");
            int len = parsedstring.Length;
            if (offset >= len)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            char c = parsedstring[offset];
            if (c == 'Z')
            {
                return timezone.ToString();
            }
            else
            {
                if (c == '+' || c == '-')
                {
                    offset++;
                    timezone.Append(c);
                    int end = offset + 5;
                    for (; offset < end; offset++)
                    {
                        c = parsedstring[offset];
                        if (char.IsDigit(c) || c == ':')
                        {
                            timezone.Append(c);
                        }
                    }
                    if (timezone.Length < 9 || timezone[6] != ':')
                    {
                        throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                    }
                }
            }
            return timezone.ToString();
        }

        public static Calendar ParseDateTime(string parsedstring, bool parseDate, bool parseTime)
        {
            if (parsedstring == null)
            {
                return null;
            }
            int offset = 0;
            int len = parsedstring.Length;
            int year = 0, month = 0, day = 0;

            if (parseDate)
            {
                offset += SkipOneCharacter(parsedstring, offset, '-', true);

                year = ParseInt(parsedstring, offset, 4);
                offset = offset + 4;

                offset += SkipOneCharacter(parsedstring, offset, '-', false);

                month = ParseInt(parsedstring, offset, 2);
                offset = offset + 2;

                offset += SkipOneCharacter(parsedstring, offset, '-', false);

                day = ParseInt(parsedstring, offset, 2);
                offset = offset + 2;

                if (parseTime)
                {
                    offset += SkipOneCharacter(parsedstring, offset, 'T', false);
                }
            }

            int hour = 0, min = 0, sec = 0, msec = 0;
            string timezone = "GMT";
            if (parseTime)
            {
                hour = ParseInt(parsedstring, offset, 2);
                offset = offset + 2;

                offset += SkipOneCharacter(parsedstring, offset, ':', false);

                min = ParseInt(parsedstring, offset, 2);
                offset = offset + 2;

                offset += SkipOneCharacter(parsedstring, offset, ':', false);

                sec = ParseInt(parsedstring, offset, 2);
                offset = offset + 2;

                if (offset < len && parsedstring[offset] == '.')
                {
                    offset++;
                    var digits = new StringBuilder();

                    for (; offset < len; offset++)
                    {
                        char c = parsedstring[offset];
                        if (char.IsDigit(c))
                        {
                            digits.Append(c);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (digits.Length > 0)
                    {
                        msec = Convert.ToInt32(digits.ToString());
                    }
                    else
                    {
                        throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                    }
                }
                timezone = ParseTimeZone(parsedstring, offset);
            }

            Calendar cal = Calendar.GetInstance(TimeZone.getTimeZone(timezone));
            cal.set(year, parseDate ? month : 0, day, hour, min, sec);
            cal.set(Calendar.MILLISECOND, msec);
            return cal;
        }

        private static void AppendInt(StringBuilder datetimestr, int num, int size, bool negative, string token)
        {
            datetimestr.Append(token);
            if (negative && num < 0)
            {
                datetimestr.Append('-');
                num = -num;
            }
            if (num >= 0)
            {
                string numstr = Convert.ToString(num);
                while (numstr.Length < size)
                {
                    datetimestr.Append('0');
                    size--;
                }

                if (numstr.Length == size)
                {
                    datetimestr.Append(numstr);
                    return;
                }
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        public static string CreateDateTimeString(Calendar cal, bool parseDate, bool parseTime)
        {
            var datetimestr = new StringBuilder();
            if (parseDate)
            {
                AppendInt(datetimestr, cal.get(Calendar.YEAR), 4, true, "");
                AppendInt(datetimestr, cal.get(Calendar.MONTH), 2, false, "-");
                AppendInt(datetimestr, cal.get(Calendar.DAY_OF_MONTH), 2, false, "-");
                if (parseTime)
                {
                    datetimestr.Append('T');
                }
            }
            if (parseTime)
            {
                AppendInt(datetimestr, cal.get(Calendar.HOUR_OF_DAY), 2, false, "");
                AppendInt(datetimestr, cal.get(Calendar.MINUTE), 2, false, ":");
                AppendInt(datetimestr, cal.get(Calendar.SECOND), 2, false, ":");
                if (cal.get(Calendar.MILLISECOND) > 0)
                {
                    AppendInt(datetimestr, cal.get(Calendar.MILLISECOND), 3, false, ".");
                }
            }

            TimeZone timezone = cal.TimeZone;
            int zoneoffset = cal.get(Calendar.ZONE_OFFSET);
            if (timezone.inDaylightTime(cal.Time))
            {
                zoneoffset += cal.get(Calendar.DST_OFFSET);
            }
            if (zoneoffset == 0)
            {
                datetimestr.Append('Z');
            }
            else
            {
                if (zoneoffset < 0)
                {
                    datetimestr.Append('-');
                    zoneoffset = -zoneoffset;
                }
                else
                {
                    datetimestr.Append('+');
                }
                int min = zoneoffset / (60 * 1000);
                int hour = zoneoffset / (60 * 60 * 1000);
                min -= hour * 60;
                AppendInt(datetimestr, hour, 2, false, "");
                AppendInt(datetimestr, min, 2, false, ":");
            }
            return datetimestr.ToString();
        }
    }
}