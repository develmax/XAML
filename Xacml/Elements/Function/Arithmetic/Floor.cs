namespace Xacml.Elements.Function.Arithmetic
{
    using System;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Floor : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:floor";
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
            if (@params.Length == paramsnum && @params[0] is DoubleDataType)
            {
                double res = ((DoubleDataType)@params[0]).Double;
                res = Math.Floor(res);
                return new DoubleDataType(Convert.ToString(res));
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}