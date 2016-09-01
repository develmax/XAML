namespace Xacml.Elements.Function.Logic
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class NOf : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:n-of";
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
            int minmum = 0;
            int size = @params.Length;
            if (size > 0)
            {
                minmum = ((IntegerDataType)@params[0]).Integer;
                size--;
                int count = 0;
                for (int i = 0; i < size && count < minmum && @params[i + 1] is BooleanDataType; i++)
                {
                    var item = (BooleanDataType)@params[i + 1];
                    if (BooleanDataType.True.Equals(item))
                    {
                        count++;
                    }
                }
                if (count == minmum) return BooleanDataType.True;
                else return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}