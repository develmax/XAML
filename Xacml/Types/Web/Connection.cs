namespace Xacml.Types.Web
{
    using System;
    using System.Net;

    using Xacml.Types.Streams;

    public class Connection
    {
        private readonly WebRequest _request;

        public Connection(Uri url)
        {
            this._request = WebRequest.Create(url);
        }

        public InputStream InputStream
        {
            get { return new InputStream(this._request.GetResponse().GetResponseStream()); }
        }
    }
}