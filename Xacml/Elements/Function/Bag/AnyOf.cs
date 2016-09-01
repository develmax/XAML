namespace Xacml.Elements.Function.Bag
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class AnyOf : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:any-of";
        internal const int paramsnum = 2;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        internal virtual string StringIdentifer
        {
            get { return stringIdentifer; }
        }

        public override URI Identifier
        {
            get { return URIID; }
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            int size = @params.Length;
            if (size == paramsnum)
            {
                if (BooleanDataType.True.Equals(@params[1]))
                {
                    return BooleanDataType.True;
                }
                else
                {
                    return BooleanDataType.False;
                }
            }
            else if (size > paramsnum && (@params[size - 1] is BagDataType))
            {
                string functionid = @params[0].Value;
                var bag = (BagDataType)@params[size - 1];
                IList children = bag.Children;
                for (int i = 1; i < size - 1; i++)
                {
                    for (int j = 0; j < children.Count; j++)
                    {
                        DataTypeValue[] newparams = { @params[i], (DataTypeValue)children[j] };
                        try
                        {
                            if (BooleanDataType.True.Equals(FunctionFactory.Evaluate(functionid, newparams, ctx)))
                            {
                                return BooleanDataType.True;
                            }
                        }
                        catch (Indeterminate ex)
                        {
                            throw new IllegalExpressionEvaluationException(ex.Message);
                        }
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