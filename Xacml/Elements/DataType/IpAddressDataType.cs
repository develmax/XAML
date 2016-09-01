namespace Xacml.Elements.DataType
{
    using System.Text;

    using Xacml.Exceptions;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class IpAddressDataType : DataTypeValue
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:2.0:data-type:ipAddress";

        private const string IpAddressPattern =
            "^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." + "([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." +
            "([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." + "([01]?\\d\\d?|2[0-4]\\d|25[0-5])$";

        private static readonly URI URIID = URI.Create(Identifer);
        private readonly string _address;
        private readonly string _mask;
        private readonly PortRange _portrange;

        public IpAddressDataType(string value)
            : base(URIID)
        {
            int maskindex = value.IndexOf('/');
            int portindex = value.IndexOf(':');

            this._portrange = null;
            this._mask = null;
            if (maskindex > 0)
            {
                this._address = value.Substring(0, maskindex).Trim();
                if (portindex > maskindex)
                {
                    this._mask = value.Substring(maskindex + 1, portindex - (maskindex + 1)).Trim();
                    this._portrange = new PortRange(value.Substring(portindex + 1).Trim());
                }
                else if (portindex < 0)
                {
                    this._mask = value.Substring(maskindex + 1).Trim();
                }
                else
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }
            }
            else
            {
                if (portindex > 0)
                {
                    this._address = value.Substring(0, portindex).Trim();
                    this._portrange = new PortRange(value.Substring(portindex + 1).Trim());
                }
                else if (portindex < 0)
                {
                    this._address = value.Trim();
                }
                else
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }
            }
            this.ValideIP();
        }

        public override string Value
        {
            get { return this._address; }
        }

        public virtual string IP
        {
            get { return this._address; }
        }

        public virtual string Mask
        {
            get { return this._mask; }
        }

        public virtual PortRange Port
        {
            get { return this._portrange; }
        }

        private void ValideIP()
        {
            //get
            {
                Pattern p = Pattern.Compile(IpAddressPattern);
                if (p.Matcher(this._address).Matches() == false)
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }
            }
        }

        public override string Encode()
        {
            var result = new StringBuilder(this._address);
            if (this._mask != null)
            {
                result.Append("/");
                result.Append(this._mask);
            }
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
            return new IpAddressDataType(value);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o is IpAddressDataType)
            {
                if (this == o)
                {
                    return true;
                }
                var ip = (IpAddressDataType)o;
                if (this._address.Equals(ip.IP) &&
                    ((this._mask == null && ip.Mask == null) || this._mask.Equals(ip.Mask)) &&
                    ((this._portrange == null && ip.Port == null) || this._portrange.Equals(ip.Port)))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 5;
            hash = 67 * hash + (this._address != null ? this._address.GetHashCode() : 0);
            hash = 67 * hash + (this._mask != null ? this._mask.GetHashCode() : 0);
            hash = 67 * hash + (this._portrange != null ? this._portrange.GetHashCode() : 0);
            return hash;
        }

        public override int CompareTo(object t)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }
    }
}