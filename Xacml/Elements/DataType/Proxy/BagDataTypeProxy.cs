namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class BagDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return BagDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return BagDataType.GetInstance(value);
        }

        #endregion
    }
}