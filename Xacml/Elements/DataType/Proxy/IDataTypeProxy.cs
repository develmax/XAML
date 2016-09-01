namespace Xacml.Elements.DataType.Proxy
{
    using Xacml.Types.Xml;

    public interface IDataTypeProxy
    {
        DataTypeValue GetInstance(Node node);
        DataTypeValue GetInstance(string value);
    }
}