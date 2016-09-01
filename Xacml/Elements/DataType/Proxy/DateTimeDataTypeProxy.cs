namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class DateTimeDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return DateTimeDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return DateTimeDataType.GetInstance(value);
        }

        #endregion
    }
}