namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class XpathExpressionDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return XpathExpressionDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return XpathExpressionDataType.GetInstance(value);
        }

        #endregion
    }
}