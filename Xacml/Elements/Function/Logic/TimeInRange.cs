namespace Xacml.Elements.Function.Logic
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class TimeInRange : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:2.0:function:time-in-range";
        internal const int paramsnum = 3;
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
            if (@params.Length == paramsnum && @params[0] is TimeDataType && @params[1] is TimeDataType &&
                @params[2] is TimeDataType)
            {
                int res1 = (@params[0]).CompareTo(@params[1]);
                int res2 = (@params[0]).CompareTo(@params[2]);
                if (res1 >= 0 && res2 < 0)
                {
                    return BooleanDataType.True;
                }
                else return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}