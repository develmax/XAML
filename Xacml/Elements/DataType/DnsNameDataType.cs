namespace Xacml.Elements.DataType
{
    using System.Text;

    using Xacml.Exceptions;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class DnsNameDataType : DataTypeValue
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:2.0:data-type:dnsName";
        private static readonly URI URIID = URI.Create(Identifer);
        private readonly string _hostname;
        private readonly PortRange _portrange;

        public DnsNameDataType(string value)
            : base(URIID)
        {
            if (value.Contains(":"))
            {
                int index = value.IndexOf(":");
                this._hostname = value.Substring(0, index).Trim();
                this._portrange = new PortRange(value.Substring(index + 1));
            }
            else
            {
                this._hostname = value.Trim();
                this._portrange = null;
            }
        }

        public override string Value
        {
            get
            {
                if (this._portrange == null)
                {
                    return this._hostname;
                }
                else
                {
                    return this._hostname + ":" + this._portrange.Encode();
                }
            }
        }

        public virtual string Hostname
        {
            get { return this._hostname; }
        }

        public virtual PortRange Port
        {
            get { return this._portrange; }
        }

        public override string Encode()
        {
            var result = new StringBuilder(this._hostname);
            if (this._portrange != null)
            {
                result.Append(":");
                result.Append(this._portrange.Encode());
            }
            return result.ToString();
        }

        public static DataTypeValue GetInstance(Node node)
        {
            return GetInstance(node.FirstChild.TextContent);
        }

        public static DataTypeValue GetInstance(string value)
        {
            return new DnsNameDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is DnsNameDataType)
            {
                if (this == o)
                {
                    return true;
                }
                var dnsname = ((DnsNameDataType)o);
                if (this._hostname.Equals(dnsname.Hostname) &&
                    ((this._portrange == null && dnsname.Port == null) || this._portrange.Equals(dnsname.Port)))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 29 * hash + (this._hostname != null ? this._hostname.GetHashCode() : 0);
            hash = 29 * hash + (this._portrange != null ? this._portrange.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }
    }
}