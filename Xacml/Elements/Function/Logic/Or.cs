namespace Xacml.Elements.Function.Logic
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Or : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:or";
        internal const int paramsnum = 0;
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
            int size = @params.Length;
            if (size > paramsnum)
            {
                int i;
                for (i = 0; @params[i] is BooleanDataType && i < size; i++)
                {
                    if (BooleanDataType.True.Equals(@params[i])) return BooleanDataType.True;
                }
                if (i == size) return BooleanDataType.False;
            }
            else return BooleanDataType.False;
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}