namespace Xacml.Elements.DataType
{
    using System.Collections;

    using Xacml.Interfaces;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public abstract class DataTypeValue : IDataType, IComparable
    {
        private readonly URI _uri;

        protected DataTypeValue(URI uri)
        {
            this._uri = uri;
        }

        public abstract string Value { get; }

        #region IComparable Members

        public abstract int CompareTo(object t);

        #endregion

        #region IDataType Members

        public virtual URI Identifier
        {
            get { return this._uri; }
        }

        public virtual bool ReturnIsBag()
        {
            return false;
        }

        public virtual IList Children
        {
            get { return new ArrayList(); }
        }

        public virtual void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            if (this.ReturnIsBag())
            {
                foreach (object o in this.Children)
                {
                    ((DataTypeValue)o).Encode(output, indenter);
                }
            }
            else
            {
                psout.PrintLine(indenter + "<AttributeValue DataType=\"" + this._uri + "\">");
                indenter.Down();
                psout.PrintLine(indenter + this.Encode());
                indenter.Up();
                psout.PrintLine((indenter + "</AttributeValue>"));
            }
        }

        #endregion

        public virtual void Encode(OutputStream output)
        {
            this.Encode(output, new Indentation(0));
        }

        public abstract string Encode();

        public abstract override bool Equals(object o);

        public abstract override int GetHashCode();
    }
}