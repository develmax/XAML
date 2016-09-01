namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class YearMonthDurationDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return YearMonthDurationDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return YearMonthDurationDataType.GetInstance(value);
        }

        #endregion
    }
}