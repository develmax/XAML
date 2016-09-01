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

    public abstract class TypeIsIn : Function
    {
        internal abstract string StringIdentifer { get; }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        protected internal virtual DataTypeValue Evaluate(DataTypeValue[] _params, EvaluationContext ctx, Type cls)
        {
            if ((_params.Length == 2) && cls.IsInstance(_params[0]) && (_params[1] is BagDataType))
            {
                IList children = _params[1].Children;
                for (int i = 0; i < children.Count; i++)
                {
                    object child = children[i];

                    if ((_params[0]).Equals(child))
                    {
                        return BooleanDataType.True;
                    }
                }
                return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(this.StringIdentifer);
        }
    }
}