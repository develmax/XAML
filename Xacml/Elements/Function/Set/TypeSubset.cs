namespace Xacml.Elements.Function.Set
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class TypeSubset : Function
    {
        public override URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        private bool isContainedInBag(BagDataType bag, DataTypeValue value)
        {
            IList children = bag.Children;
            for (int j = 0; j < children.Count; j++)
            {
                var child2 = (DataTypeValue)children[j];
                if (value.Equals(child2))
                {
                    return true;
                }
            }
            return false;
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            if (@params.Length == 2 && @params[0] is BagDataType && @params[1] is BagDataType)
            {
                IList children0 = @params[0].Children;

                for (int i = 0; i < children0.Count; i++)
                {
                    var child1 = (DataTypeValue)children0[i];
                    if (this.isContainedInBag((BagDataType)@params[1], child1) == false)
                    {
                        return BooleanDataType.False;
                    }
                }
                return BooleanDataType.True;
            }
            throw new IllegalExpressionEvaluationException("TypeSubset");
        }
    }
}