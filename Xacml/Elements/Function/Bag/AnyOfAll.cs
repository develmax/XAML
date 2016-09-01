namespace Xacml.Elements.Function.Bag
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class AnyOfAll : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:any-of-all";
        internal const int paramsnum = 3;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        internal virtual string StringIdentifer
        {
            get { return stringIdentifer; }
        }

        public override URI Identifier
        {
            get { return URIID; }
        }

        private bool IsOneOfBag(string functionid, DataTypeValue one, BagDataType bag, EvaluationContext ctx)
        {
            IList children = bag.Children;
            for (int j = 0; j < children.Count; j++)
            {
                DataTypeValue[] newparams = { one, (DataTypeValue)children[j] };
                try
                {
                    if (BooleanDataType.False.Equals(FunctionFactory.Evaluate(functionid, newparams, ctx)))
                    {
                        return false;
                    }
                }
                catch (Indeterminate ex)
                {
                    throw new IllegalExpressionEvaluationException(ex.Message);
                }
            }
            return true;
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            int size = @params.Length;
            if (size == paramsnum && (@params[1] is BagDataType) && (@params[2] is BagDataType))
            {
                string functionid = @params[0].Value;
                var bag1 = (BagDataType)@params[1];
                var bag2 = (BagDataType)@params[2];
                IList children = bag1.Children;
                for (int i = 0; i < children.Count; i++)
                {
                    if (this.IsOneOfBag(functionid, (DataTypeValue)children[i], bag2, ctx))
                    {
                        return BooleanDataType.True;
                    }
                }
                return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }
    }
}