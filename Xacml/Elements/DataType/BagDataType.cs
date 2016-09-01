namespace Xacml.Elements.DataType
{
    using System.Collections;
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;

    public class BagDataType : DataTypeValue
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:x:data-type:bag";
        public static readonly URI URIID = URI.Create(Identifer);

        private readonly IList<DataTypeValue> _bag;

        public BagDataType()
            : base(URIID)
        {
            this._bag = new List<DataTypeValue>();
        }

        public bool Empty
        {
            get { return this.IsEmpty; }
        }

        public override string Value
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public override IList Children
        {
            get { return (IList)this._bag; }
        }

        public virtual bool IsOneAndOnlyOne
        {
            get { return this._bag.Count == 1; }
        }

        public virtual bool IsEmpty
        {
            get { return this._bag.Count == 0; }
        }

        public static DataTypeValue GetInstance(Node node)
        {
            throw new UnsupportedOperationException("Not yet implemented");
        }

        public static DataTypeValue GetInstance(string value)
        {
            throw new UnsupportedOperationException("Not yet implemented");
        }

        public virtual void AddDataType(DataTypeValue value)
        {
            this._bag.Add(value);
        }

        public override string Encode()
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (o is BagDataType)
            {
                if (this == o) return true;
                if (this._bag.Equals(((BagDataType)o).Children)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 17 * hash + (this._bag != null ? this._bag.GetHashCode() : 0);
            return hash;
        }

        public override bool ReturnIsBag()
        {
            return true;
        }

        public override int CompareTo(object t)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }
    }
}