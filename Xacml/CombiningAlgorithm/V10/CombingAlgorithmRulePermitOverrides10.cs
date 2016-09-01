namespace Xacml.CombiningAlgorithm.V10
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class CombingAlgorithmRulePermitOverrides10 : ICombiningAlgorithm
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:1.0:rule-combining-algorithm:permit-overrides";

        #region ICombiningAlgorithm Members

        public void Evaluate(EvaluationContext ctx, IPolicyLanguageModel[] evals, string ID)
        {
            if (ctx.GetResult(ID).HasDecision())
            {
                return;
            }

            bool atLeastOneError = false;
            bool potentialPermit = false;
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
                    ctx.GetResult(ID).Decision = Decision.Permit;
                    ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(evals[i].ElementId));
                    return;
                }

                if (_Decision.IsDecisionIndeterminateD)
                {
                    atLeastOneError = true;
                    continue;
                }

                if (_Decision.IsDecisionIndeterminateP)
                {
                    atLeastOneError = true;
                    potentialPermit = true;
                    continue;
                }
            }
            if (potentialPermit)
            {
                ctx.GetResult(ID).Decision = Decision.Indeterminate;
            }
            else if (atLeastOneDeny)
            {
                ctx.GetResult(ID).Decision = Decision.Deny;
                ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(DenyID));
            }
            else if (atLeastOneError)
            {
                ctx.GetResult(ID).Decision = Decision.Indeterminate;
            }
            else
            {
                ctx.GetResult(ID).Decision = Decision.NotApplicable;
            }
        }

        #endregion
    }
}