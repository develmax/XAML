namespace Xacml.Elements.Policy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xacml.CombiningAlgorithm;
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class PolicySet : IPolicyLanguageModel
    {
        public const string stringIdentifer = "PolicySet";
        private readonly AdviceExpressions _adviceExpressions;
        private readonly Description _description;
        private readonly int _maxDelegationDepth = -1;
        private readonly ObligationExpressions _obligationExpressions;
        private readonly IList<IPolicyLanguageModel> _policies;
        private readonly string _policyCombiningAlgId;
        private readonly PolicyIssuer _policyIssuer;
        private readonly PolicySetDefaults _policySetDefaults;
        private readonly string _policySetId;
        private readonly Target _target;
        private readonly string _version;

        public PolicySet(Node node)
        {
            this._policySetId = this.GetNodeAttribute(node, "PolicySetId", true);
            this._version = this.GetNodeAttribute(node, "Version", true);
            this._policyCombiningAlgId = this.GetNodeAttribute(node, "PolicyCombiningAlgId", true);
            string MaxDelegationDepthStr = this.GetNodeAttribute(node, "MaxDelegationDepth", false);
            if (MaxDelegationDepthStr != null)
            {
                this._maxDelegationDepth = Convert.ToInt32(MaxDelegationDepthStr);
            }

            this._policies = new List<IPolicyLanguageModel>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                string name = child.NodeName;
                if (name.Equals(Description.stringIdentifer))
                {
                    this._description = (Description)PolicyElementFactory.GetInstance(child);
                }
                else if (name.Equals(PolicyIssuer.stringIdentifer))
                {
                    this._policyIssuer = (PolicyIssuer)PolicyElementFactory.GetInstance(child);
                }
                else if (name.Equals(Target.stringIdentifer))
                {
                    this._target = (Target)PolicyElementFactory.GetInstance(child);
                }
                else if (name.Equals(PolicySetDefaults.stringIdentifer))
                {
                    this._policySetDefaults = (PolicySetDefaults)PolicyElementFactory.GetInstance(child);
                }
                else if (name.Equals(stringIdentifer))
                {
                    var ps = (PolicySet)PolicyElementFactory.GetInstance(child);
                    PolicySchema.AddPolicyLanguageModel(ps.ElementId, ps);
                    this._policies.Add(ps);
                }
                else if (name.Equals(Policy.stringIdentifer))
                {
                    var p = (Policy)PolicyElementFactory.GetInstance(child);
                    PolicySchema.AddPolicyLanguageModel(p.ElementId, p);
                    this._policies.Add(p);
                }
                else if (name.Equals(PolicyIdReference.Identifer))
                {
                    var pref = (PolicyIdReference)PolicyElementFactory.GetInstance(child);
                    this._policies.Add(pref);
                }
                else if (name.Equals(ObligationExpressions.stringIdentifer))
                {
                    this._obligationExpressions = ((ObligationExpressions)PolicyElementFactory.GetInstance(child));
                }
                else if (name.Equals(PolicySetIdReference.Identifer))
                {
                    var psref = (PolicySetIdReference)PolicyElementFactory.GetInstance(child);
                    this._policies.Add(psref);
                }
                else if (name.Equals(PolicyCombinerParameters.stringIdentifer))
                {
                    var pcp = (PolicyCombinerParameters)PolicyElementFactory.GetInstance(child);
                    this._policies.Add(pcp);
                }
                else if (name.Equals(PolicySetCombinerParameters.stringIdentifer))
                {
                    var pscp = (PolicySetCombinerParameters)PolicyElementFactory.GetInstance(child);
                    this._policies.Add(pscp);
                }
                else if (name.Equals(CombinerParameters.stringIdentifer))
                {
                    var cp = (CombinerParameters)PolicyElementFactory.GetInstance(child);
                    this._policies.Add(cp);
                }
                else if (name.Equals(AdviceExpressions.stringIdentifer))
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
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<PolicySet PolicySetId=\"" + this._policySetId + "\"");
            psout.PrintLine(indenter + "Version=\"" + this._version + "\"");
            psout.PrintLine(indenter + "PolicyCombiningAlgId=\"" + this._policyCombiningAlgId + "\"");
            if (this._maxDelegationDepth >= 0)
            {
                psout.PrintLine(indenter + "MaxDelegationDepth=\"" + this._maxDelegationDepth + "\"");
            }
            psout.PrintLine(indenter + ">");

            indenter.Down();
            if (this._description != null)
            {
                this._description.Encode(output, indenter);
            }
            this._target.Encode(output, indenter);
            if (this._policyIssuer != null)
            {
                this._policyIssuer.Encode(output, indenter);
            }
            if (this._policySetDefaults != null)
            {
                this._policySetDefaults.Encode(output, indenter);
            }

            foreach (IPolicyLanguageModel eval in this._policies)
            {
                eval.Encode(output, indenter);
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
            psout.PrintLine(indenter + "</PolicySet>");
        }

        public void Evaluate(EvaluationContext ctx, string SchemeID)
        {
            if (ctx.GetResult(this._policySetId).HasDecision())
            {
                return;
            }
            try
            {
                if (BooleanDataType.True.Equals(this._target.Evaluate(ctx, this._policySetId)))
                {
                    CombiningAlgorithmFactory.Evaluate(
                        this._policyCombiningAlgId, ctx, this._policies.ToArray(), this._policySetId);
                    return;
                }
            }
            catch (Indeterminate)
            {
                ctx.GetResult(this._policySetId).Decision = Decision.Indeterminate;
            }
        }

        public string ElementId
        {
            get { return this._policySetId; }
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
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

        public static IElement GetInstance(Node node)
        {
            return new PolicySet(node);
        }

        public static IElement GetInstance(string stream)
        {
            return new PolicySet(NodeFactory.GetInstanceFromString(stream));
        }
    }
}