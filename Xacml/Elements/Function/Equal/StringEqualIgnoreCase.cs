namespace Xacml.Elements.Function.Equal
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class StringEqualIgnoreCase : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:string-equal-ignore-case";
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
            if (@params.Length == paramsnum && @params[0] is StringDataType && @params[1] is StringDataType)
            {
                string arg1 = (@params[0]).Value.ToLower();
                string arg2 = (@params[1]).Value.ToLower();
                if (arg1.Equals(arg2))
                {
                    return BooleanDataType.True;
                }
                return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}