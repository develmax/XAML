namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class AnyURIDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return AnyURIDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return AnyURIDataType.GetInstance(value);
        }

        #endregion
    }
}