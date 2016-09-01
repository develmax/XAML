namespace Xacml.CombiningAlgorithm.V30
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class CombingAlgorithmRuleOrderedPermitOverrides30 : ICombiningAlgorithm
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:3.0:rule-combining-algorithm:ordered-permit-overrides";

        #region ICombiningAlgorithm Members

        public void Evaluate(EvaluationContext ctx, IPolicyLanguageModel[] evals, string ID)
        {
            if (ctx.GetResult(ID).HasDecision())
            {
                return;
            }
            bool atLeastOneErrorD = false;
            bool atLeastOneErrorP = false;
            bool atLeastOneErrorDP = false;
            bool atLeastOneDeny = false;

            string DenyID = null;
            for (int i = 0; i < evals.Length; i++)
            {
                evals[i].Evaluate(ctx, ID);
                Decision _Decision = ctx.GetResult(evals[i].ElementId).Decision;

                if (_Decision.IsDeny)
                {
                    atLeastOneDeny = true;
                    DenyID = evals[i].ElementId;
                    continue;
                }

                if (_Decision.IsPermit)
                {
                    ctx.GetResult(ID).Decision = new Decision(Decision.DecisionPermit);
                    ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(evals[i].ElementId));
                    return;
                }

                if (_Decision.IsDecisionIndeterminateD)
                {
                    atLeastOneErrorD = true;
                    continue;
                }

                if (_Decision.IsDecisionIndeterminateP)
                {
                    atLeastOneErrorP = true;
                    continue;
                }

                if (_Decision.IsDecisionIndeterminateDP)
                {
                    atLeastOneErrorDP = true;
                    continue;
                }
            }

            if (atLeastOneErrorDP)
            {
                ctx.GetResult(ID).Decision = Decision.IndeterminateDP;
            }
            else if (atLeastOneErrorD && (atLeastOneErrorP || atLeastOneDeny))
            {
                ctx.GetResult(ID).Decision = Decision.IndeterminateDP;
            }
            else if (atLeastOneErrorP)
            {
                ctx.GetResult(ID).Decision = Decision.IndeterminateP;
            }
            else if (atLeastOneDeny)
            {
                ctx.GetResult(ID).Decision = Decision.Deny;
                ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(DenyID));
            }
            else if (atLeastOneErrorD)
            {
                ctx.GetResult(ID).Decision = Decision.IndeterminateD;
            }
            else
            {
                ctx.GetResult(ID).Decision = Decision.NotApplicable;
            }
        }

        #endregion
    }
}