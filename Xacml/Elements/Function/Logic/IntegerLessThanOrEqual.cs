namespace Xacml.Elements.Function.Logic
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class IntegerLessThanOrEqual : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:integer-less-than-or-equal";
        internal const int paramsnum = 2;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        public override URI Identifier
        {
            get { return URIID; }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            if (@params.Length == paramsnum && @params[0] is IntegerDataType && @params[1] is IntegerDataType)
            {
                bool res = ((IntegerDataType)@params[0]).Integer <= ((IntegerDataType)@params[1]).Integer;
                if (res)
                {
                    return BooleanDataType.True;
                }
                else return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}