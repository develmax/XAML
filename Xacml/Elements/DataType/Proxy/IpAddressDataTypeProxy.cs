namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class IpAddressDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return IpAddressDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return IpAddressDataType.GetInstance(value);
        }

        #endregion
    }
}