namespace Xacml.Elements.Function.String
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class StringNormalizeToLowercase : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:string-normalize-to-lower-case";
        internal const int paramsnum = 1;
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
            if (@params.Length == paramsnum && @params[0] is StringDataType)
            {
                string res = (@params[0]).Value.Trim().ToLower();
                return new StringDataType(res);
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}