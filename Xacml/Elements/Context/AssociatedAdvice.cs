namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class AssociatedAdvice : IElement
    {
        public const string Identifer = "AssociatedAdvice";
        private readonly List<Advice> _Advices;

        public AssociatedAdvice()
        {
            this._Advices = new List<Advice>();
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<AssociatedAdvice>");
            indenter.Down();
            foreach (Advice advice in this._Advices)
            {
                advice.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</AssociatedAdvice>");
        }

        #endregion

        public virtual void AddAdvice(Advice advice)
        {
            this._Advices.Add(advice);
        }
    }
}