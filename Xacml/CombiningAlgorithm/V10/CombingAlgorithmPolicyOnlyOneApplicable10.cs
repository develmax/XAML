namespace Xacml.CombiningAlgorithm.V10
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class CombingAlgorithmPolicyOnlyOneApplicable10 : ICombiningAlgorithm
    {
        public const string Identifer = "urn:oasis:names:tc:xacml:1.0:policy-combining-algorithm:only-one-applicable";

        #region ICombiningAlgorithm Members

        public void Evaluate(EvaluationContext ctx, IPolicyLanguageModel[] evals, string ID)
        {
            if (ctx.GetResult(ID).HasDecision())
            {
                return;
            }

            bool atLeastOne = false;

            string DecisionID = null;
            for (int i = 0; i < evals.Length; i++)
            {
                evals[i].Evaluate(ctx, ID);
                Decision _Decision = ctx.GetResult(evals[i].ElementId).Decision;
                if (_Decision.IsPermit)
                {
                    if (atLeastOne)
                    {
                        ctx.GetResult(ID).Decision = Decision.Indeterminate;
                        return;
                    }
                    else
                    {
                        atLeastOne = true;
                        ctx.GetResult(ID).Decision = Decision.Permit;
                        DecisionID = evals[i].ElementId;
                    }
                }

                if (_Decision.IsDeny)
                {
                    if (atLeastOne)
                    {
                        ctx.GetResult(ID).Decision = Decision.Indeterminate;
                        return;
                    }
                    else
                    {
                        atLeastOne = true;
                        ctx.GetResult(ID).Decision = Decision.Deny;
                        DecisionID = evals[i].ElementId;
                    }
                }

                if (_Decision.IsIndeterminate)
                {
                    ctx.GetResult(ID).Decision = Decision.Indeterminate;
                    return;
                }
            }

            if (ctx.GetResult(ID).Decision.IsPermit || ctx.GetResult(ID).Decision.IsDeny)
            {
                ctx.GetResult(ID).PassObligationsAndAdvices(ctx.GetResult(DecisionID));
            }
            else
            {
                ctx.GetResult(ID).Decision = Decision.NotApplicable;
            }
        }

        #endregion
    }
}