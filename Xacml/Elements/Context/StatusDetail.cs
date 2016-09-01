namespace Xacml.Elements.Context
{
    using System.Collections.Generic;

    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class StatusDetail : IElement
    {
        public const string Identifer = "StatusDetail";
        private readonly List<MissingAttributeDetail> _missingAttributeDetails;

        public StatusDetail(string detail)
        {
            if (StatusCode.StatusCodeMissingAttribute.Equals(detail))
            {
                this._missingAttributeDetails = new List<MissingAttributeDetail>();
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
            }
        }

        #region IElement Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            if (this._missingAttributeDetails.Count == 0)
            {
                psout.PrintLine(indenter + "<StatusDetail/>");
            }
            else
            {
                psout.PrintLine(indenter + "<StatusDetail>");
                indenter.Down();
                foreach (MissingAttributeDetail m in this._missingAttributeDetails)
                {
                    m.Encode(output, indenter);
                }
                indenter.Up();
                psout.PrintLine(indenter + "</StatusDetail>");
            }
        }

        #endregion

        public virtual void AddMissingAttributeDetail(MissingAttributeDetail m)
        {
            this._missingAttributeDetails.Add(m);
        }
    }
}