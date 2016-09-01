namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class Attributes : IElement
    {
        public const string Identifer = "Attributes";
        private readonly IList<Attribute> _attributes;
        private readonly AnyURIDataType _category;
        private readonly Content _content;

        public Attributes(Node node)
        {
            this._category = new AnyURIDataType(node.Attributes.GetNamedItem("Category").NodeValue);
            if (this._category == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            this._attributes = new List<Attribute>();
            NodeList children = node.ChildNodes;
            this._content = null;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals("Attribute"))
                {
                    this._attributes.Add(Attribute.GetInstance(child));
                }
                else if (child.NodeName.Equals("Content"))
                {
                    this._content = Content.GetInstance(child);
                }
            }
        }

        public virtual Content Content
        {
            get { return this._content; }
        }

        public virtual AnyURIDataType Category
        {
            get { return this._category; }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Attributes Category=\"" + this._category.Encode() + "\"> ");
            indenter.Down();
            if (this._content != null)
            {
                this._content.Encode(output, indenter);
            }
            foreach (Attribute attr in this._attributes)
            {
                attr.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Attributes>");
        }

        #endregion

        public static Attributes GetInstance(Node node)
        {
            return new Attributes(node);
        }

        public virtual BagDataType Evaluate(string Category, string AttributeId, string DataType)
        {
            if (this._category.Value.Equals(Category))
            {
                foreach (Attribute attr in this._attributes)
                {
                    BagDataType ret = attr.Evaluate(AttributeId, DataType);
                    if (!ret.Empty)
                    {
                        return ret;
                    }
                }
            }
            return new BagDataType();
        }
    }
}