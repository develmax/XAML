namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Elements.DataType;
    using Xacml.Elements.Policy;
    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class Attribute : IElement
    {
        public const string Identifer = "Attribute";

        private readonly AnyURIDataType _attributeId;
        private readonly List<AttributeValue> _attributeValues;
        private readonly BooleanDataType _includeInResult;
        private readonly StringDataType _issuer;

        public Attribute(Node node)
        {
            if (!node.NodeName.Equals(Identifer))
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            NamedNodeMap attributes = node.Attributes;

            Node idnode = attributes.GetNamedItem("AttributeId");
            if (idnode != null)
            {
                this._attributeId = new AnyURIDataType(idnode.NodeValue);
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            Node includeinresultnode = attributes.GetNamedItem("IncludeInResult");
            if (includeinresultnode != null)
            {
                string value = includeinresultnode.NodeValue.Trim();
                if (value.EqualsIgnoreCase("true"))
                {
                    this._includeInResult = BooleanDataType.True;
                }
                else
                {
                    this._includeInResult = BooleanDataType.False;
                }
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            Node issuernode = attributes.GetNamedItem("Issuer");
            if (issuernode != null)
            {
                this._issuer = new StringDataType(issuernode.NodeValue);
            }

            this._attributeValues = new List<AttributeValue>();

            NodeList children = node.ChildNodes;

            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals("AttributeValue"))
                {
                    this._attributeValues.Add((AttributeValue)AttributeValue.GetInstance(child));
                }
            }

            if (this._attributeValues.Count == 0)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
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
                indenter + "<Attribute AttributeId=\"" + this._attributeId.Encode() + "\" " + "IncludeInResult=\"" +
                this._includeInResult.Encode() + "\"");
            if (this._issuer != null)
            {
                psout.Print(" Issuer=\"" + this._issuer.Encode() + "\"");
            }

            psout.PrintLine(">");
            indenter.Down();
            foreach (AttributeValue a in this._attributeValues)
            {
                a.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Attribute>");
        }

        #endregion

        public static Attribute GetInstance(Node node)
        {
            return new Attribute(node);
        }

        public virtual BagDataType Evaluate(string attributeId, string dataType)
        {
            var bag = new BagDataType();
            foreach (AttributeValue attr in this._attributeValues)
            {
                if ((this._attributeId.Value.Equals(attributeId)) && attr.DataType.Equals(dataType))
                {
                    bag.AddDataType(attr.DataTypeValue);
                }
            }
            return bag;
        }
    }
}