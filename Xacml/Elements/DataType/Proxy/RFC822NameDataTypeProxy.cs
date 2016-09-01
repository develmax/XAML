namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public class RFC822NameDataTypeProxy : IDataTypeProxy
    {
        #region IDataTypeProxy Members

        public DataTypeValue GetInstance(Node node)
        {
            return RFC822NameDataType.GetInstance(node);
        }

        public DataTypeValue GetInstance(string value)
        {
            return RFC822NameDataType.GetInstance(value);
        }

        #endregion
    }
}