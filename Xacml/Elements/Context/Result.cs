namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Result : IElement
    {
        public const string Identifer = "Result";
        private readonly List<Attributes> _attributes;
        private readonly PolicyIdentifierList _policyIdentifierList;
        private readonly Status _status;
        private AssociatedAdvice _associatedAdvices;
        private Decision _decision;
        private Obligations _obligations;

        public Result()
        {
            this._decision = Decision.WaitForDecision;
            this._attributes = new List<Attributes>();
            this._obligations = null;
            this._associatedAdvices = null;
            this._policyIdentifierList = null;
            this._status = null;
        }

        public virtual Decision Decision
        {
            get { return this._decision; }
            set { this._decision = value; }
        }

        public virtual AssociatedAdvice AssociatedAdvice
        {
            set { this._associatedAdvices = value; }
        }

        public virtual Obligations Obligations
        {
            set { this._obligations = value; }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Result>");
            indenter.Down();
            this._decision.Encode(output, indenter);
            if (this._status != null)
            {
                this._status.Encode(output, indenter);
            }
            if (this._obligations != null)
            {
                this._obligations.Encode(output, indenter);
            }
            if (this._associatedAdvices != null)
            {
                this._associatedAdvices.Encode(output, indenter);
            }
            if (this._policyIdentifierList != null)
            {
                this._policyIdentifierList.Encode(output, indenter);
            }
            foreach (Attributes attrs in this._attributes)
            {
                attrs.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Result>");
        }

        #endregion

        public virtual void PassObligationsAndAdvices(Result r)
        {
            this._obligations = r._obligations;
            this._associatedAdvices = r._associatedAdvices;
        }


        public virtual bool HasDecision()
        {
            return Decision.WaitForDecision.Equals(this._decision) == false;
        }
    }
}