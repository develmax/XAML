namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class DnsNameDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return DnsNameDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return DnsNameDataType.GetInstance(value);
        }

        #endregion
    }
}