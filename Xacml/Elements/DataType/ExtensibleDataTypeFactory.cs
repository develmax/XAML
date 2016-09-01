namespace Xacml.Elements.DataType
{
    using System.Collections;

    using Xacml.Elements.DataType.Proxy;
    using Xacml.Exceptions;
    using Xacml.Types.Web;

    public class ExtensibleDataTypeFactory : DataTypeFactory
    {
        private readonly IDictionary _datatypes;

        public ExtensibleDataTypeFactory(IDictionary supporteddatatypes)
        {
            this._datatypes = new Hashtable();
            IEnumerator it = supporteddatatypes.Keys.GetEnumerator();
            while (it.MoveNext())
            {
                string id = it.Current.ToString();
                object obj = supporteddatatypes[id];
                this._datatypes.Add(id, obj);
            }
        }

        public override void AddDataType(string id, IDataTypeProxy proxy)
        {
            if (this._datatypes.Contains(id) == false)
            {
                this._datatypes.Add(id, proxy);
            }
        }

        public override DataTypeValue CreateValue(URI datatype, string value)
        {
            if (this._datatypes.Contains(datatype.ToString()))
            {
                var proxy = (IDataTypeProxy)this._datatypes[datatype.ToString()];
                return proxy.GetInstance(value);
            }
            else throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        public override DataTypeValue CreateValue(string datatype, string value)
        {
            try
            {
                var uri = new URI(datatype);
                return this.CreateValue(uri, value);
            }
            catch (URISyntaxException)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }
    }
}