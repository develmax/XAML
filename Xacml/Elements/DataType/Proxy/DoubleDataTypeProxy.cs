namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class DoubleDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return DoubleDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return DoubleDataType.GetInstance(value);
        }

        #endregion
    }
}