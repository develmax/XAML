namespace Xacml
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;
    using Xacml.Engine;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Utils;

    public class Xacml3
    {
        private readonly PEP _PEP;
        private PolicySchema ps;

        public Xacml3()
        {
            this._PEP = new PEP();
        }

        public virtual Response Evaluate(string reqfile)
        {
            Request request = null;

            try
            {
                try
                {
                    request = Request.GetInstance(new FileInputStream(reqfile));
                    //request.encode(new OutputStream(System.Console.Out), new Indentation());
                }
                catch (FileNotFoundException ex)
                {
                    Logger.GetLogger(typeof(Xacml3).Name).Log(null, ex);
                }
            }
            catch (Indeterminate ex)
            {
                Logger.GetLogger(typeof(Xacml3).Name).Log(null, ex);
            }

            return this._PEP.Evaluate(request, this.ps);
        }

        public virtual void InitializePolicy(string policyDir, string policyID)
        {
            this.ps = PolicySchema.GetInstance(policyID);

            PolicySchema.AddPolicyLanguageModelFromDir(policyDir);

            //ps.encode(new OutputStream(System.Console.Out), new Indentation());
        }
    }
}