namespace Xacml.Elements.Function.Bag
{
    using System;
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Utils;

    public abstract class BagSize : Function
    {
        internal abstract string StringIdentifer { get; }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            if (@params.Length == 1 && @params[0] is BagDataType)
            {
                IList children = @params[0].Children;
                return new IntegerDataType(Convert.ToString(children.Count));
            }
            throw new IllegalExpressionEvaluationException(this.StringIdentifer);
        }
    }
}