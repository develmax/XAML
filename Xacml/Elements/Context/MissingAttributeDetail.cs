namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Elements.Policy;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class MissingAttributeDetail : IElement
    {
        public const string Identifer = "MissingAttributeDetail";
        private readonly string _attributeId;
        private readonly List<AttributeValue> _attributeValues;
        private readonly string _category;
        private readonly string _dataType;
        private readonly string _issuer;

        public MissingAttributeDetail(string Category, string AttributeId, string DataType)
        {
            this._category = Category;
            this._attributeId = AttributeId;
            this._dataType = DataType;
            this._issuer = null;
            this._attributeValues = new List<AttributeValue>();
        }

        public MissingAttributeDetail(string Category, string AttributeId, string DataType, string Issuer)
        {
            this._category = Category;
            this._attributeId = AttributeId;
            this._dataType = DataType;
            this._issuer = Issuer;
            this._attributeValues = new List<AttributeValue>();
        }

        public MissingAttributeDetail(
            string Category, string AttributeId, string DataType, string Issuer, List<AttributeValue> AttributeValues)
        {
            this._category = Category;
            this._attributeId = AttributeId;
            this._dataType = DataType;
            this._issuer = Issuer;
            this._attributeValues = new List<AttributeValue>();
            foreach (AttributeValue a in AttributeValues)
            {
                this._attributeValues.Add(a);
            }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.Print(
                indenter + "<MissingAttributeDetail Category=\"" + this._category + "\" AttributeId=\"" +
                this._attributeId + "\" DataType=\"" + this._dataType + "\"");
            if (this._issuer == null)
            {
                psout.PrintLine(">");
            }
            else
            {
                psout.PrintLine(" Issuer=\"" + this._issuer + "\">");
            }
            indenter.Down();
            foreach (AttributeValue a in this._attributeValues)
            {
                a.Encode(output, indenter);
            }
            indenter.Up();
            psout.Print(indenter + "</MissingAttributeDetail>");
        }

        #endregion

        public virtual void addAttributeValue(AttributeValue values)
        {
            this._attributeValues.Add(values);
        }
    }
}