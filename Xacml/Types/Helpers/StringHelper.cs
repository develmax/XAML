namespace Xacml.Types.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        public static bool EqualsIgnoreCase(this string str, string str2)
        {
            return str.ToLower() == str2.ToLower();
        }

        public static bool matches(this string str, string str2)
        {
            return true;
        }

        public static string[] StringSplit(this string source, string regexDelimiter, bool trimTrailingEmptyStrings)
        {
            string[] splitArray = Regex.Split(source, regexDelimiter);

            if (trimTrailingEmptyStrings)
            {
                if (splitArray.Length > 1)
                {
                    for (int i = splitArray.Length; i > 0; i--)
                    {
                        if (splitArray[i - 1].Length > 0)
                        {
                            if (i < splitArray.Length) Array.Resize(ref splitArray, i);

                            break;
                        }
                    }
                }
            }

            return splitArray;
        }
    }
}