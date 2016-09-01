namespace Xacml.Engine
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class PEP
    {
        public virtual Response Evaluate(Request req, PolicySchema ps)
        {
            var pdp = new PDP();

            Result r = pdp.Evaluate(req, ps);

            this.DoObligations(r);
            this.DoAssociatedAdvice(r);

            var resp = new Response();

            resp.XMLHeaderAttributes = req.XMLHeaderAttributes;
            resp.AddResult(r);

            return resp;
        }

        private void DoObligations(Result r)
        {
            //TODO
            r.Obligations = null;
        }

        private void DoAssociatedAdvice(Result r)
        {
            //TODO
            r.AssociatedAdvice = null;
        }
    }
}