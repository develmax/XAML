namespace Xacml.Elements.Policy
{
    using System.Collections.Generic;
    using System.Text;

    using Xacml.CombiningAlgorithm;
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class Policy : IPolicyLanguageModel
    {
        public const string stringIdentifer = "Policy";
        private readonly AdviceExpressions _adviceExpressions;
        private readonly Description _description;
        private readonly string _maxDelegationDepth;
        private readonly ObligationExpressions _obligationExpressions;
        private readonly PolicyDefaults _policyDefaults;
        private readonly string _policyId;
        private readonly PolicyIssuer _policyIssuer;
        private readonly List<IPolicyLanguageModel> _policyLanguageModels;
        private readonly string _ruleCombiningAlgId;
        private readonly Target _target;
        private readonly Dictionary<string, VariableDefinition> _variableDefinitions;
        private readonly string _version;

        public Policy(Node node)
        {
            this._policyId = this.GetNodeAttribute(node, "PolicyId", true);
            this._version = this.GetNodeAttribute(node, "Version", true);
            this._ruleCombiningAlgId = this.GetNodeAttribute(node, "RuleCombiningAlgId", true);
            this._maxDelegationDepth = this.GetNodeAttribute(node, "MaxDelegationDepth", false);

            this._policyLanguageModels = new List<IPolicyLanguageModel>();
            this._variableDefinitions = new Dictionary<string, VariableDefinition>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(Target.stringIdentifer))
                {
                    this._target = (Target)PolicyElementFactory.GetInstance(child);
                }
                else if (child.NodeName.Equals(Rule.stringIdentifer))
                {
                    var r = (Rule)PolicyElementFactory.GetInstance(child);
                    r.PolicyParentID = this._policyId;
                    PolicySchema.AddPolicyLanguageModel(r.ElementId, r);
                    this._policyLanguageModels.Add(r);
                }
                else if (child.NodeName.Equals(Description.stringIdentifer))
                {
                    this._description = (Description)PolicyElementFactory.GetInstance(child);
                }
                else if (child.NodeName.Equals(PolicyIssuer.stringIdentifer))
                {
                    this._policyIssuer = (PolicyIssuer)PolicyElementFactory.GetInstance(child);
                }
                else if (child.NodeName.Equals(PolicyDefaults.stringIdentifer))
                {
                    this._policyDefaults = (PolicyDefaults)PolicyElementFactory.GetInstance(child);
                }
                else if (child.NodeName.Equals(VariableDefinition.stringIdentifer))
                {
                    var vd = (VariableDefinition)PolicyElementFactory.GetInstance(child);
                    this._variableDefinitions.Add(vd.VariableId, vd);
                }
                else if (child.NodeName.Equals(RuleCombinerParameters.stringIdentifer))
                {
                    var rcp = ((RuleCombinerParameters)PolicyElementFactory.GetInstance(child));
                    this._policyLanguageModels.Add(rcp);
                }
                else if (child.NodeName.Equals(CombinerParameters.stringIdentifer))
                {
                    var cp = ((CombinerParameters)PolicyElementFactory.GetInstance(child));
                    this._policyLanguageModels.Add(cp);
                }
                else if (child.NodeName.Equals(ObligationExpressions.stringIdentifer))
                {
                    this._obligationExpressions = (((ObligationExpressions)PolicyElementFactory.GetInstance(child)));
                }
                else if (child.NodeName.Equals(AdviceExpressions.stringIdentifer))
                {
                    this._adviceExpressions = ((AdviceExpressions)PolicyElementFactory.GetInstance(child));
                }
            }
            if (this._target == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IPolicyLanguageModel Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var str = new StringBuilder();
            var psout = new PrintStream(output);
            str.Append(indenter.ToString());
            str.Append("<Policy PolicyId=\"");
            str.Append(this._policyId);
            str.Append("\" ");
            str.Append("Version=\"");
            str.Append(this._version);
            str.Append("\" ");
            str.Append("RuleCombiningAlgId=\"");
            str.Append(this._ruleCombiningAlgId);
            str.Append("\" ");
            if (this._maxDelegationDepth != null)
            {
                str.Append("MaxDelegationDepth=\"");
                str.Append(this._maxDelegationDepth);
                str.Append("\" ");
            }
            str.Append(">");
            psout.PrintLine(str.ToString());
            indenter.Down();

            foreach (VariableDefinition var in this._variableDefinitions.Values)
            {
                var.Encode(output, indenter);
            }

            this._target.Encode(output, indenter);
            if (this._policyDefaults != null)
            {
                this._policyDefaults.Encode(output, indenter);
            }
            if (this._description != null)
            {
                this._description.Encode(output, indenter);
            }
            if (this._policyIssuer != null)
            {
                this._policyIssuer.Encode(output, indenter);
            }
            foreach (IPolicyLanguageModel o in this._policyLanguageModels)
            {
                o.Encode(output, indenter);
            }
            if (this._obligationExpressions != null)
            {
                this._obligationExpressions.Encode(output, indenter);
            }
            if (this._adviceExpressions != null)
            {
                this._adviceExpressions.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Policy>");
        }

        public void Evaluate(EvaluationContext ctx, string SchemeID)
        {
            if (ctx.GetResult(this._policyId).HasDecision())
            {
                return;
            }

            try
            {
                if (BooleanDataType.True.Equals(this._target.Evaluate(ctx, this._policyId)))
                {
                    CombiningAlgorithmFactory.Evaluate(
                        this._ruleCombiningAlgId, ctx, this._policyLanguageModels.ToArray(), this._policyId);
                    this.EvaluateObligations(ctx, SchemeID);
                    return;
                }

                ctx.GetResult(this._policyId).Decision = Decision.NotApplicable;
                return;
            }
            catch (Indeterminate)
            {
                ctx.GetResult(this._policyId).Decision = Decision.Indeterminate;
            }
        }

        public string ElementId
        {
            get { return this._policyId; }
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            if (this._variableDefinitions.ContainsKey(variableID))
            {
                return this._variableDefinitions[variableID];
            }
            throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
        }

        #endregion

        private string GetNodeAttribute(Node node, string tag, bool isRequired)
        {
            NamedNodeMap attrs = node.Attributes;
            Node attr = attrs.GetNamedItem(tag);
            if (attr != null)
            {
                return attr.NodeValue;
            }

            if (isRequired)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            else
            {
                return null;
            }
        }

        private void EvaluateObligations(EvaluationContext ctx, string SchemeID)
        {
            string effect = ctx.GetResult(this._policyId).Decision.DecisionString;
            if (this._obligationExpressions != null)
            {
                var obligations = (Obligations)this._obligationExpressions.Evaluate(ctx, SchemeID, effect);
                ctx.GetResult(this.ElementId).Obligations = obligations;
            }
        }

        public static IElement GetInstance(Node node)
        {
            return new Policy(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Policy(NodeFactory.GetInstanceFromString(value));
        }
    }
}