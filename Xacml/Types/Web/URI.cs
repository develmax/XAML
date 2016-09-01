namespace Xacml.Types.Web
{
    public class URI
    {
        private readonly string _uri;

        public URI(string uri)
        {
            this._uri = uri;
        }

        public static URI Create(string uri)
        {
            return new URI(uri);
        }

        public override string ToString()
        {
            return this._uri;
        }
    }
}