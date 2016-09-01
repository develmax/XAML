namespace Xacml.Elements.Function.Bag
{
    using System;
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Streams;
    using Xacml.Utils;

    public abstract class OneAndOnly : Function
    {
        internal abstract int Paramsnum { get; }
        internal abstract string StringIdentifer { get; }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        protected internal virtual DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx, Type cls)
        {
            if (@params.Length == this.Paramsnum && @params[0] is BagDataType)
            {
                IList children = @params[0].Children;
                if (children.Count == 1)
                {
                    object child = children[0];
                    if (cls.IsInstance(child))
                    {
                        return (DataTypeValue)child;
                    }
                }
            }
            throw new IllegalExpressionEvaluationException(this.StringIdentifer);
        }
    }
}