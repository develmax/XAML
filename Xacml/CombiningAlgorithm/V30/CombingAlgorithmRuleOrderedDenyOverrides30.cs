namespace Xacml.CombiningAlgorithm.V30
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class CombingAlgorithmRuleOrderedDenyOverrides30 : ICombiningAlgorithm
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:3.0:rule-combining-algorithm:ordered-deny-overrides";

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
            bool atLeastOnePermit = false;

            string PermitID = null;
            for (int i = 0; i < evals.Length; i++)
            {
                evals[i].Evaluate(ctx, ID);
                Decision _Decision = ctx.GetResult(evals[i].ElementId).Decision;

                if (_Decision.IsDeny)
                {
                    ctx.GetResult(ID).Decision = Decision.Deny;
                    ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(evals[i].ElementId));
                    return;
                }

                if (_Decision.IsPermit)
                {
                    atLeastOnePermit = true;
                    PermitID = evals[i].ElementId;
                    continue;
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
            else if (atLeastOneErrorD && (atLeastOneErrorP || atLeastOnePermit))
            {
                ctx.GetResult(ID).Decision = Decision.IndeterminateDP;
            }
            else if (atLeastOneErrorD)
            {
                ctx.GetResult(ID).Decision = Decision.IndeterminateD;
            }
            else if (atLeastOnePermit)
            {
                ctx.GetResult(ID).Decision = Decision.Permit;
                ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(PermitID));
            }
            else if (atLeastOneErrorP)
            {
                ctx.GetResult(ID).Decision = Decision.IndeterminateP;
            }
            else
            {
                ctx.GetResult(ID).Decision = Decision.NotApplicable;
            }
        }

        #endregion
    }
}