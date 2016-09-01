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

    public class Rule : IPolicyLanguageModel
    {
        public const string stringIdentifer = "Rule";
        private readonly AdviceExpressions _adviceExpressions;
        private readonly Condition _condition;
        private readonly Description _description;
        private readonly Effect _effect;
        private readonly bool _isRuleReference;
        private readonly ObligationExpressions _obligationExpressions;
        private readonly StringDataType _ruleId;
        private readonly Target _target;
        private string _policyParentIdForVariableDefinition;

        public Rule(Node node)
        {
            Node ruleidnode = node.Attributes.GetNamedItem("RuleId");
            this._ruleId =
                (StringDataType)DataTypeFactory.Instance.CreateValue(StringDataType.URIID, ruleidnode.NodeValue);
            Node effectnode = node.Attributes.GetNamedItem("Effect");
            this._effect = new Effect(effectnode.NodeValue);
            NodeList children = node.ChildNodes;

            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(Target.stringIdentifer))
                {
                    this._target = (Target)PolicyElementFactory.GetInstance(child);
                }
                if (child.NodeName.Equals(Description.stringIdentifer))
                {
                    this._description = (Description)PolicyElementFactory.GetInstance(child);
                }
                if (child.NodeName.Equals(Condition.stringIdentifer))
                {
                    this._condition = (Condition)PolicyElementFactory.GetInstance(child);
                }
                if (child.NodeName.Equals(ObligationExpressions.stringIdentifer))
                {
                    this._obligationExpressions = ((ObligationExpressions)PolicyElementFactory.GetInstance(child));
                }
                if (child.NodeName.Equals(AdviceExpressions.stringIdentifer))
                {
                    this._adviceExpressions = ((AdviceExpressions)PolicyElementFactory.GetInstance(child));
                }
            }
            if (this._target == null && this._condition == null)
            {
                this._isRuleReference = true;
            }
        }

        public virtual string PolicyParentID
        {
            set { this._policyParentIdForVariableDefinition = value; }
        }

        public virtual string Effect
        {
            get { return this._effect.ToString(); }
        }

        #region IPolicyLanguageModel Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(
                indenter + "<Rule RuleId=\"" + this._ruleId.Encode() + "\" Effect=\"" + this._effect + "\">");
            if (this._isRuleReference == false)
            {
                indenter.Down();
                if (this._description != null)
                {
                    this._description.Encode(output, indenter);
                }
                if (this._target != null)
                {
                    this._target.Encode(output, indenter);
                }
                if (this._condition != null)
                {
                    this._condition.Encode(output, indenter);
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
            }
            psout.PrintLine(indenter + "</Rule>");
        }

        public void Evaluate(EvaluationContext ctx, string SchemeID)
        {
            if (ctx.GetResult(this.ElementId).HasDecision())
            {
                return;
            }
            try
            {
                if (this._isRuleReference)
                {
                    try
                    {
                        var r = (Rule)PolicySchema.GetPolicyLanguageModel(this.ElementId);
                        r.Evaluate(ctx, SchemeID);
                        this.EvaluateObligations(ctx, SchemeID);
                        return;
                    }
                    catch (Indeterminate)
                    {
                        if (this._effect.isPermit)
                        {
                            ctx.GetResult(this.ElementId).Decision = Decision.IndeterminateP;
                        }
                        else
                        {
                            ctx.GetResult(this.ElementId).Decision = Decision.IndeterminateD;
                        }
                        return;
                    }
                }
                if (BooleanDataType.True.Equals(this._target.Evaluate(ctx, this._ruleId.Value)))
                {
                    if (this._condition == null ||
                        BooleanDataType.True.Equals(this._condition.Evaluate(ctx, this._ruleId.Value)))
                    {
                        if (this._effect.isPermit)
                        {
                            ctx.GetResult(this.ElementId).Decision = Decision.Permit;
                        }
                        else
                        {
                            ctx.GetResult(this.ElementId).Decision = Decision.Deny;
                        }
                        this.EvaluateObligations(ctx, SchemeID);
                        return;
                    }
                }

                ctx.GetResult(this.ElementId).Decision = Decision.NotApplicable;
                return;
            }
            catch (Indeterminate)
            {
                if (this._effect.isPermit)
                {
                    ctx.GetResult(this.ElementId).Decision = Decision.IndeterminateP;
                }
                else
                {
                    ctx.GetResult(this.ElementId).Decision = Decision.IndeterminateD;
                }
            }
        }

        public string ElementId
        {
            get { return this._ruleId.Value; }
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            if (this._policyParentIdForVariableDefinition != null)
            {
                return
                    PolicySchema.GetPolicyLanguageModel(this._policyParentIdForVariableDefinition).GetVariableDefinition
                        (variableID);
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new Rule(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Rule(NodeFactory.GetInstanceFromString(value));
        }

        private void EvaluateObligations(EvaluationContext ctx, string SchemeID)
        {
            if (this._obligationExpressions != null)
            {
                var obligations =
                    (Obligations)this._obligationExpressions.Evaluate(ctx, SchemeID, this._effect.ToString());
                ctx.GetResult(this.ElementId).Obligations = obligations;
            }
        }
    }
}