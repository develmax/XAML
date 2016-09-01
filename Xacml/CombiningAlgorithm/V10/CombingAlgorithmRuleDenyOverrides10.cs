namespace Xacml.CombiningAlgorithm.V10
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class CombingAlgorithmRuleDenyOverrides10 : ICombiningAlgorithm
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:1.0:rule-combining-algorithm:deny-overrides";

        #region ICombiningAlgorithm Members

        public void Evaluate(EvaluationContext ctx, IPolicyLanguageModel[] evals, string ID)
        {
            if (ctx.GetResult(ID).HasDecision())
            {
                return;
            }

            bool atLeastOneError = false;
            bool potentialDeny = false;
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
                    atLeastOneError = true;
                    potentialDeny = true;
                    continue;
                }

                if (_Decision.IsDecisionIndeterminateP)
                {
                    atLeastOneError = true;
                    continue;
                }
            }

            if (potentialDeny)
            {
                ctx.GetResult(ID).Decision = Decision.Indeterminate;
            }
            else if (atLeastOnePermit)
            {
                ctx.GetResult(ID).Decision = Decision.Permit;
                ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(PermitID));
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