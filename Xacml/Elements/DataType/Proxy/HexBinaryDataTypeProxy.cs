namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class HexBinaryDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return HexBinaryDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return HexBinaryDataType.GetInstance(value);
        }

        #endregion
    }
}