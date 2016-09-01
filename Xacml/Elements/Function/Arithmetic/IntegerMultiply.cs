namespace Xacml.Elements.Function.Arithmetic
{
    using System;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class IntegerMultiply : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:integer-multiply";
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
            int result = 1;
            int size = @params.Length;
            if (size >= paramsnum)
            {
                for (int i = 0; i < size; i++)
                {
                    result *= ((IntegerDataType)@params[i]).Integer;
                }
                return new IntegerDataType(Convert.ToString(result));
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}