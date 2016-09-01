namespace Xacml.Elements.Function.Set
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class TypeSetEquals : Function
    {
        public override URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        private bool isContainedInBag(IList children, DataTypeValue value)
        {
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
                IList children1 = @params[1].Children;
                if (children0.Count != children1.Count)
                {
                    return BooleanDataType.False;
                }
                for (int i = 0; i < children0.Count; i++)
                {
                    var child = (DataTypeValue)children0[i];
                    if (this.isContainedInBag(children1, child) == false)
                    {
                        return BooleanDataType.False;
                    }
                }
                return BooleanDataType.True;
            }
            throw new IllegalExpressionEvaluationException("TypeSetEquals");
        }
    }
}