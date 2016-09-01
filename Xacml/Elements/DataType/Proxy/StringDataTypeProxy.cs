namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class StringDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return StringDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return StringDataType.GetInstance(value);
        }

        #endregion
    }
}