namespace Xacml.Elements.DataType
{
    using Xacml.Elements.DataType.Proxy;
    using Xacml.Types.Web;

    public abstract class DataTypeFactory
    {
        private static readonly IDataTypeFactoryProxy _defaultDataTypeFactory;

        static DataTypeFactory()
        {
            _defaultDataTypeFactory = (IDataTypeFactoryProxy)StandardDataTypeFactory.Factory;
        }

        public static DataTypeFactory Instance
        {
            get { return _defaultDataTypeFactory.Factory; }
        }

        public abstract void AddDataType(string id, IDataTypeProxy proxy);
        public abstract DataTypeValue CreateValue(string datatype, string value);
        public abstract DataTypeValue CreateValue(URI datatype, string value);
    }
}