namespace Xacml.Engine
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class PDP
    {
        public virtual Result Evaluate(Request req, PolicySchema ps)
        {
            var ctx = new EvaluationContext(req);

            ps.evluate(ctx);

            ctx.RootPolicyId = ps.RootElementID;

            return ctx.GetResult(ps.RootElementID);
        }
    }
}