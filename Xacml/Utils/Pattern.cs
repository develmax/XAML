namespace Xacml.Utils
{
    using System.Text.RegularExpressions;

    public class Pattern
    {
        private readonly Regex _regex;

        private Pattern(Regex regex)
        {
            this._regex = regex;
        }

        public static Pattern Compile(string pattern)
        {
            return new Pattern(new Regex(pattern));
        }

        public Matcher Matcher(string address)
        {
            return new Matcher(this._regex.Match(address));
        }
    }
}