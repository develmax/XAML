namespace Xacml.Elements.Function.Bag
{
    using System;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Streams;
    using Xacml.Utils;

    public abstract class TypeBag : Function
    {
        internal abstract string StringIdentifer { get; }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        protected internal virtual DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx, Type cls)
        {
            var bag = new BagDataType();
            for (int i = 0; i < @params.Length; i++)
            {
                DataTypeValue param = @params[i];
                if (cls.IsInstance(param))
                {
                    bag.AddDataType(param);
                }
                else
                {
                    throw new IllegalExpressionEvaluationException(this.StringIdentifer);
                }
            }
            return bag;
        }
    }
}