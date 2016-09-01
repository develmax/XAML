namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class RuleCombinerParameters : IPolicyLanguageModel
    {
        public const string stringIdentifer = "RuleCombinerParameters";
        private readonly string _ruleIdRef;

        public RuleCombinerParameters(Node node)
        {
            Node attr = node.Attributes.GetNamedItem("RuleIdRef");
            if (attr != null)
            {
                this._ruleIdRef = attr.NodeValue;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IPolicyLanguageModel Members

        public void Evaluate(EvaluationContext ctx, string SchemeID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public string ElementId
        {
            get { return this._ruleIdRef; }
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<RuleCombinerParameters RuleIdRef=\"" + this._ruleIdRef + "\" />");
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new RuleCombinerParameters(node);
        }

        public static IElement GetInstance(string value)
        {
            return new RuleCombinerParameters(NodeFactory.GetInstanceFromString(value));
        }
    }
}