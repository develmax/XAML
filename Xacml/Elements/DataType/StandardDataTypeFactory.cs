namespace Xacml.Elements.DataType
{
    using System.Collections;

    using Xacml.Elements.DataType.Proxy;

    public class StandardDataTypeFactory : ExtensibleDataTypeFactory, IDataTypeFactoryProxy
    {
        private static StandardDataTypeFactory _factoryInstance;
        private static Hashtable _supportedDatatypes;

        public StandardDataTypeFactory()
            : base(_supportedDatatypes)
        {
        }

        public static DataTypeFactory Factory
        {
            get
            {
                if (_factoryInstance == null)
                {
                    InitializeSupportedDataType();
                    _factoryInstance = new StandardDataTypeFactory();
                }
                return _factoryInstance;
            }
        }

        #region IDataTypeFactoryProxy Members

        DataTypeFactory IDataTypeFactoryProxy.Factory
        {
            get { return Factory; }
        }

        #endregion

        public static void InitializeSupportedDataType()
        {
            _supportedDatatypes =
                new Hashtable
                    {
                        { StringDataType.Identifer, new StringDataTypeProxy() },
                        { BooleanDataType.Identifer, new BooleanDataTypeProxy() },
                        { IntegerDataType.Identifer, new IntegerDataTypeProxy() },
                        { DoubleDataType.Identifer, new DoubleDataTypeProxy() },
                        { DateTimeDataType.Identifer, new DateTimeDataTypeProxy() },
                        { AnyURIDataType.Identifer, new AnyURIDataTypeProxy() },
                        { HexBinaryDataType.Identifer, new HexBinaryDataTypeProxy() },
                        { Base64BinaryDataType.Identifer, new Base64BinaryDataTypeProxy() },
                        { DayTimeDurationDataType.Identifer, new DayTimeDurationDataTypeProxy() },
                        { YearMonthDurationDataType.Identifer, new YearMonthDurationDataTypeProxy() },
                        { X500NameDataType.Identifer, new X500NameDataTypeProxy() },
                        { RFC822NameDataType.Identifer, new RFC822NameDataTypeProxy() },
                        { IpAddressDataType.Identifer, new IpAddressDataTypeProxy() },
                        { DnsNameDataType.Identifer, new DnsNameDataTypeProxy() },
                        { XpathExpressionDataType.Identifer, new XpathExpressionDataTypeProxy() },
                        { DateDataType.Identifer, new DateDataTypeProxy() },
                        { BagDataType.Identifer, new BagDataTypeProxy() }
                    };
        }
    }
}