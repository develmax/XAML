namespace Xacml.Elements.Context
{
    using System.Collections;
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Response : IElement
    {
        public const string Identifer = "Response";

        private readonly IList<Result> _results;
        private IDictionary _otherAttributes;

        public Response()
        {
            this._results = new List<Result>();
        }

        public virtual IDictionary XMLHeaderAttributes
        {
            set { this._otherAttributes = new Hashtable(value); }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine("<Response ");
            IEnumerator itr = this._otherAttributes.Keys.GetEnumerator();
            while (itr.MoveNext())
            {
                var name = (string)itr.Current;
                psout.PrintLine(name + "=\"" + this._otherAttributes[name] + "\" ");
            }
            psout.PrintLine(">");
            indenter.Down();
            foreach (Result r in this._results)
            {
                r.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine("</Response>");
        }

        #endregion

        public virtual void AddResult(Result r)
        {
            this._results.Add(r);
        }

        public virtual IList<Result> GetResults()
        {
            return _results;
        }
    }
}