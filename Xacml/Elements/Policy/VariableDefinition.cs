namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class VariableDefinition : IEvaluatable
    {
        public const string stringIdentifer = "VariableDefinition";
        private readonly IEvaluatable _evaluatable;
        private readonly string _variableId;

        public VariableDefinition(Node node)
        {
            Node VariableIdNode = node.Attributes.GetNamedItem("VariableId");
            if (VariableIdNode != null)
            {
                this._variableId = VariableIdNode.NodeValue;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            this._evaluatable = null;
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeType == Node.ELEMENT_NODE)
                {
                    IElement e = PolicyElementFactory.GetInstance(children.Item(i));
                    if (e is IEvaluatable)
                    {
                        this._evaluatable = (IEvaluatable)e;
                        break;
                    }
                }
            }
            if (this._evaluatable == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        public virtual string VariableId
        {
            get { return this._variableId; }
        }

        #region IEvaluatable Members

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            return this._evaluatable.Evaluate(ctx, SchemeID);
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<VariableDefinition VariableId=\"" + this._variableId + "\">");
            indenter.Down();
            this._evaluatable.Encode(output, indenter);
            indenter.Up();
            psout.PrintLine(indenter + "</VariableDefinition>");
        }

        #endregion

        public static IEvaluatable GetInstance(Node node)
        {
            return new VariableDefinition(node);
        }

        public static IElement GetInstance(string value)
        {
            return new VariableDefinition(NodeFactory.GetInstanceFromString(value));
        }
    }
}