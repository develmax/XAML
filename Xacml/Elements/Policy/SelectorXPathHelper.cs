namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Elements.Function;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class SelectorXPathHelper : XPathHelper
    {
        public override URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }
    }
}