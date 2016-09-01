namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class DayTimeDurationDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return DayTimeDurationDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return DayTimeDurationDataType.GetInstance(value);
        }

        #endregion
    }
}