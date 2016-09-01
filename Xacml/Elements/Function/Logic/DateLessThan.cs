namespace Xacml.Elements.Function.Logic
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class DateLessThan : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:date-less-than";
        internal const int paramsnum = 2;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

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
            if (@params.Length == paramsnum && @params[0] is DateDataType && @params[1] is DateDataType)
            {
                int res = (@params[0]).CompareTo(@params[1]);
                if (res < 0)
                {
                    return BooleanDataType.True;
                }
                else return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}