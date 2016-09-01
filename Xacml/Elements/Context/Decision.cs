namespace Xacml.Elements.Context
{
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Decision : IElement
    {
        public const string DecisionPermit = "Permit";
        public const string DecisionDeny = "Deny";
        public const string DecisionIndeterminate = "Indeterminate";
        public const string DecisionNotApplicable = "NotApplicable";
        public const string DecisionIndeterminateD = "IndeterminateD";
        public const string DecisionIndeterminateP = "IndeterminateP";
        public const string DecisionIndeterminateDP = "IndeterminateDP";
        public const string DecisionWaitForDecision = "NoDecision";

        public static readonly Decision Permit = new Decision(DecisionPermit);
        public static readonly Decision Deny = new Decision(DecisionDeny);
        public static readonly Decision Indeterminate = new Decision(DecisionIndeterminate);
        public static readonly Decision NotApplicable = new Decision(DecisionNotApplicable);
        public static readonly Decision IndeterminateD = new Decision(DecisionIndeterminateD);
        public static readonly Decision IndeterminateP = new Decision(DecisionIndeterminateP);
        public static readonly Decision IndeterminateDP = new Decision(DecisionIndeterminateDP);
        public static readonly Decision WaitForDecision = new Decision(DecisionWaitForDecision);

        private readonly string _decision;

        public Decision()
        {
        }

        public Decision(string decision)
        {
            this._decision = decision;
        }

        public virtual bool IsPermit
        {
            get { return this._decision.Equals(DecisionPermit); }
        }

        public virtual bool IsDeny
        {
            get { return this._decision.Equals(DecisionDeny); }
        }

        public virtual bool IsIndeterminate
        {
            get
            {
                return
                    this._decision.Equals(DecisionIndeterminate) ||
                    this._decision.Equals(DecisionIndeterminateD) ||
                    this._decision.Equals(DecisionIndeterminateP) ||
                    this._decision.Equals(DecisionIndeterminateDP);
            }
        }

        public virtual bool IsNotApplicable
        {
            get { return this._decision.Equals(DecisionNotApplicable); }
        }

        public virtual bool IsDecisionIndeterminateD
        {
            get { return this._decision.Equals(DecisionIndeterminateD); }
        }

        public virtual bool IsDecisionIndeterminateP
        {
            get { return this._decision.Equals(DecisionIndeterminateP); }
        }

        public virtual bool IsDecisionIndeterminateDP
        {
            get { return this._decision.Equals(DecisionIndeterminateDP); }
        }

        public virtual string DecisionString
        {
            get { return this._decision; }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);

            psout.PrintLine(indenter + "<Decision>" + this._decision + "</Decision>");
        }

        #endregion

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            else if (o is Decision)
            {
                if (this == o)
                {
                    return true;
                }
                else if (this._decision.Equals(((Decision)o).DecisionString))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            hash = 83 * hash + (this._decision != null ? this._decision.GetHashCode() : 0);
            return hash;
        }
    }
}