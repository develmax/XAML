namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class PolicyCombinerParameters : IPolicyLanguageModel
    {
        public const string stringIdentifer = "PolicyCombinerParameters";
        private readonly string _policyIdRef;

        public PolicyCombinerParameters(Node node)
        {
            Node attr = node.Attributes.GetNamedItem("PolicyIdRef");
            if (attr != null)
            {
                this._policyIdRef = attr.NodeValue;
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
            get { return this._policyIdRef; }
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<PolicyCombinerParameters PolicyIdRef=\"" + this._policyIdRef + "\" />");
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new PolicyCombinerParameters(node);
        }

        public static IElement GetInstance(string value)
        {
            return new PolicyCombinerParameters(NodeFactory.GetInstanceFromString(value));
        }
    }
}