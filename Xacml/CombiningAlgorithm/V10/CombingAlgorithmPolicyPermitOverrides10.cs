namespace Xacml.CombiningAlgorithm.V10
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class CombingAlgorithmPolicyPermitOverrides10 : ICombiningAlgorithm
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:1.0:policy-combining-algorithm:permit-overrides";

        #region ICombiningAlgorithm Members

        public void Evaluate(EvaluationContext ctx, IPolicyLanguageModel[] evals, string ID)
        {
            if (ctx.GetResult(ID).HasDecision())
            {
                return;
            }

            bool atLeastOneDeny = false;
            bool atLeastOneError = false;

            string DenyID = null;
            for (int i = 0; i < evals.Length; i++)
            {
                evals[i].Evaluate(ctx, ID);
                Decision _Decision = ctx.GetResult(evals[i].ElementId).Decision;
                if (_Decision.IsPermit)
                {
                    ctx.GetResult(ID).Decision = Decision.Permit;
                    ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(evals[i].ElementId));
                    return;
                }

                if (_Decision.IsDeny)
                {
                    atLeastOneDeny = true;
                    DenyID = evals[i].ElementId;
                    continue;
                }

                if (_Decision.IsIndeterminate)
                {
                    atLeastOneError = true;
                    continue;
                }
            }
            if (atLeastOneDeny)
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