namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class PolicySetIdReference : IPolicyLanguageModel
    {
        public const string Identifer = "PolicySetIdReference";

        private readonly string _url;

        public PolicySetIdReference(Node node)
        {
            this._url = (node.TextContent.Trim());
        }

        #region IPolicyLanguageModel Members

        public void Evaluate(EvaluationContext ctx, string SchemeID)
        {
            try
            {
                var p = (PolicySet)PolicySchema.GetPolicyLanguageModel(this._url);
                p.Evaluate(ctx, SchemeID);
            }
            catch (Indeterminate)
            {
                ctx.GetResult(this._url).Decision = Decision.Indeterminate;
            }
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<PolicySetIdReference >");
            psout.PrintLine(indenter + this._url);
            psout.PrintLine(indenter + "</PolicySetIdReference >");
        }

        public string ElementId
        {
            get { return this._url; }
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new PolicySetIdReference(node);
        }

        public static IElement GetInstance(string value)
        {
            return new PolicySetIdReference(NodeFactory.GetInstanceFromString(value));
        }
    }
}