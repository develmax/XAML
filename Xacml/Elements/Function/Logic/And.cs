namespace Xacml.Elements.Function.Logic
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class And : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:and";
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
            int size = @params.Length;
            if (size >= paramsnum)
            {
                int i;
                for (i = 0; i < size && @params[i] is BooleanDataType; i++)
                {
                    if (BooleanDataType.False.Equals(@params[i])) return BooleanDataType.False;
                }
                if (i == 0 || i == size) return BooleanDataType.True;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}