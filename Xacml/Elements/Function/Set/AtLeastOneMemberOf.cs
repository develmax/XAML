namespace Xacml.Elements.Function.Set
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class AtLeastOneMemberOf : Function
    {
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
            if (@params.Length == 2 && @params[0] is BagDataType && @params[1] is BagDataType)
            {
                IList children0 = @params[0].Children;
                IList children1 = @params[1].Children;
                for (int i = 0; i < children0.Count; i++)
                {
                    var child1 = (DataTypeValue)children0[i];
                    for (int j = 0; j < children0.Count; j++)
                    {
                        var child2 = (DataTypeValue)children1[j];
                        if (child1.Equals(child2))
                        {
                            return BooleanDataType.True;
                        }
                    }
                }
                return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException("AtLeastOneMemberOf");
        }
    }
}