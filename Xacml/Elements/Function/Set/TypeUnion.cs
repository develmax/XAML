namespace Xacml.Elements.Function.Set
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class TypeUnion : Function
    {
        public override URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        private void addDataType(BagDataType bag, DataTypeValue value)
        {
            IList children = bag.Children;
            for (int j = 0; j < children.Count; j++)
            {
                var child = (DataTypeValue)children[j];
                if (child.Equals(value))
                {
                    return;
                }
            }
            bag.AddDataType(value);
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            if (@params.Length >= 2)
            {
                var bag = new BagDataType();
                for (int i = 0; @params[i] is BagDataType && i < @params.Length; i++)
                {
                    IList children = @params[i].Children;
                    for (int j = 0; j < children.Count; j++)
                    {
                        var value = (DataTypeValue)children[j];
                        this.addDataType(bag, value);
                    }
                }
                return bag;
            }
            throw new IllegalExpressionEvaluationException("TypeUnion");
        }
    }
}