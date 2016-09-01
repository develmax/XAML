namespace Xacml.Utils
{
    using System.Text.RegularExpressions;

    public class Matcher
    {
        private readonly Match _match;

        public Matcher(Match match)
        {
            this._match = match;
        }

        public bool Matches()
        {
            return this._match.Success;
        }
    }
}