namespace Xacml.Elements.Function.Logic
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Not : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:not";
        internal const int paramsnum = 1;
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
            if (@params.Length == paramsnum && @params[0] is BooleanDataType)
            {
                bool res = ((BooleanDataType)@params[0]).Boolean;
                if (res) return BooleanDataType.False;
                else return BooleanDataType.True;
            }
            throw new IllegalExpressionEvaluationException("String Identifer");
        }
    }
}