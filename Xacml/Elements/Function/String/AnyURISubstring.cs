namespace Xacml.Elements.Function.String
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class AnyURISubstring : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:3.0:function:anyURI-substring";
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
            if (@params.Length == paramsnum && @params[0] is AnyURIDataType && @params[1] is IntegerDataType &&
                @params[2] is IntegerDataType)
            {
                string arg1 = (@params[0]).Value;
                int len = arg1.Length;
                int start = ((IntegerDataType)@params[1]).Integer;
                int end = ((IntegerDataType)@params[2]).Integer;
                if (start < len && end < len)
                {
                    string sub = arg1.Substring(start, end - start);
                    return new StringDataType(sub);
                }
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}