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

    public class VariableReference : IEvaluatable
    {
        public const string stringIdentifer = "VariableReference";
        private readonly string _variableId;

        public VariableReference(Node node)
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
        }

        #region IEvaluatable Members

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            VariableDefinition p = PolicySchema.GetPolicyLanguageModel(SchemeID).GetVariableDefinition(this._variableId);
            return p.Evaluate(ctx, SchemeID);
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<VariableReference VariableId=\"" + this._variableId + "\"/>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new VariableReference(node);
        }

        public static IElement GetInstance(string value)
        {
            return new VariableReference(NodeFactory.GetInstanceFromString(value));
        }
    }
}