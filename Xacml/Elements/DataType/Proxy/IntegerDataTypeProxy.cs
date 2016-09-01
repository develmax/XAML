namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class IntegerDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return IntegerDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return IntegerDataType.GetInstance(value);
        }

        #endregion
    }
}