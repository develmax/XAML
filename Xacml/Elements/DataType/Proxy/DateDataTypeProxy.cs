namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class DateDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return DateDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return DateDataType.GetInstance(value);
        }

        #endregion
    }
}