namespace Xacml.Types.Web
{
    using System;

    public class URL
    {
        private readonly Uri _url;

        public URL(string url)
        {
            this._url = new Uri(url);
        }

        public Connection openConnection()
        {
            return new Connection(this._url);
        }
    }
}