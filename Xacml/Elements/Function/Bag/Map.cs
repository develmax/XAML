namespace Xacml.Elements.Function.Bag
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Map : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:map";
        internal const int paramsnum = 2;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        public override URI Identifier
        {
            get { return URIID; }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        private void ExcuteBag(string functionid, BagDataType input, BagDataType output, EvaluationContext ctx)
        {
            IList children = input.Children;
            for (int i = 0; i < children.Count; i++)
            {
                this.ExcuteOne(functionid, (DataTypeValue)children[i], output, ctx);
            }
        }

        private void ExcuteOne(string functionid, DataTypeValue input, BagDataType output, EvaluationContext ctx)
        {
            DataTypeValue[] newparams = { input };
            try
            {
                output.AddDataType(FunctionFactory.Evaluate(functionid, newparams, ctx));
            }
            catch (Indeterminate ex)
            {
                throw new IllegalExpressionEvaluationException(ex.Message);
            }
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            int size = @params.Length;
            if (size >= paramsnum)
            {
                var bag = new BagDataType();
                string functionid = @params[0].Value;
                for (int i = 1; i < size; i++)
                {
                    if (@params[i] is BagDataType)
                    {
                        this.ExcuteBag(functionid, (BagDataType)@params[i], bag, ctx);
                    }
                    else
                    {
                        this.ExcuteOne(functionid, @params[i], bag, ctx);
                    }
                }
                return bag;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}