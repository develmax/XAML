namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Obligations : IElement
    {
        public const string Identifer = "Obligations";
        private readonly List<Obligation> _obligations;

        public Obligations()
        {
            this._obligations = new List<Obligation>();
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Obligations>");
            indenter.Down();
            foreach (Obligation o in this._obligations)
            {
                o.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Obligations>");
        }

        #endregion

        public virtual void addObligation(Obligation o)
        {
            this._obligations.Add(o);
        }
    }
}