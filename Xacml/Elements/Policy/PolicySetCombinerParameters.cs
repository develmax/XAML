namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class PolicySetCombinerParameters : IPolicyLanguageModel
    {
        public const string stringIdentifer = "PolicySetCombinerParameters";
        private readonly string _policySetIdRef;

        public PolicySetCombinerParameters(Node node)
        {
            Node attr = node.Attributes.GetNamedItem("PolicySetIdRef");
            if (attr != null)
            {
                this._policySetIdRef = attr.NodeValue;
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
            get { return this._policySetIdRef; }
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(
                indenter + "<PolicySetCombinerParameters PolicySetIdRef=\"" + this._policySetIdRef + "\" />");
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new PolicySetCombinerParameters(node);
        }

        public static IElement GetInstance(string value)
        {
            return new PolicySetCombinerParameters(NodeFactory.GetInstanceFromString(value));
        }
    }
}