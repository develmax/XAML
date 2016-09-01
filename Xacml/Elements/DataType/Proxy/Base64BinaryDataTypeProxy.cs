namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class Base64BinaryDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return Base64BinaryDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return Base64BinaryDataType.GetInstance(value);
        }

        #endregion
    }
}