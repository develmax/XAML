namespace Xacml.Elements.Function.Arithmetic
{
    using System;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class DoubleSubtract : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:double-subtract";
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
            if (@params.Length == paramsnum && @params[0] is DoubleDataType && @params[1] is DoubleDataType)
            {
                double? res = ((DoubleDataType)@params[0]).Double - ((DoubleDataType)@params[1]).Double;
                return new DoubleDataType(Convert.ToString(res));
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}