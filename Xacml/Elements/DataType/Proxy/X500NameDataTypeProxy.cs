namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class X500NameDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return X500NameDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return X500NameDataType.GetInstance(value);
        }

        #endregion
    }
}