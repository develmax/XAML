namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class BooleanDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return BooleanDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return BooleanDataType.GetInstance(value);
        }

        #endregion
    }
}