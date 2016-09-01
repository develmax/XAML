namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class EvaluationContext : IElement
    {
        public const string Identifer = "EvaluationContext";

        private static readonly URI _URIID = URI.Create("EvaluationContext");

        private readonly DateDataType _currentdate;
        private readonly DateTimeDataType _currentdatetime;
        private readonly TimeDataType _currenttime;
        private readonly Request _request;
        private readonly IDictionary<string, Result> _results;
        private string _rootPolicyId;

        public EvaluationContext(Request req)
        {
            this._request = req;
            this._results = new Dictionary<string, Result>();
            this._currentdate = new DateDataType();
            this._currenttime = new TimeDataType();
            this._currentdatetime = new DateTimeDataType();
        }

        public EvaluationContext(EvaluationContext context)
        {
            this._request = new Request(context._request);
            this._results = new Dictionary<string, Result>();
            this._currentdate = context._currentdate;
            this._currenttime = context._currenttime;
            this._currentdatetime = context._currentdatetime;
            this._rootPolicyId = context._rootPolicyId;
        }

        public virtual string RootPolicyId
        {
            set { this._rootPolicyId = value; }
            get { return this._rootPolicyId; }
        }

        public virtual Node ContentRoot
        {
            get
            {
                if (this._request == null)
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }

                Content content = this._request.Content;
                return content.ContentRoot;
            }
        }

        public virtual Request Request
        {
            get
            {
                if (this._request == null)
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }

                return this._request;
            }
        }

        public virtual TimeDataType CurrentTime
        {
            get { return this._currenttime; }
        }

        public virtual DateDataType CurrentDate
        {
            get { return this._currentdate; }
        }

        public virtual DateTimeDataType CurrentDateTime
        {
            get { return this._currentdatetime; }
        }

        #region IElement Members

        public URI Identifier
        {
            get { return _URIID; }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        #endregion

        public virtual Result GetResult(string ID)
        {
            if (this._results.ContainsKey(ID))
            {
                return this._results[ID];
            }
            else
            {
                var res = new Result();
                this._results.Add(ID, res);
                return res;
            }
        }

        public virtual BagDataType Evaluate(
            string Category, string AttributeId, string DataType, string MustBePresent, string Issuer)
        {
            if (this._request == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            return this._request.Evaluate(this, Category, AttributeId, DataType, MustBePresent, Issuer);
        }
    }
}