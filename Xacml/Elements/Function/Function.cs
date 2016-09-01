namespace Xacml.Elements.Function
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public abstract class Function : IElement
    {
        #region IElement Members

        public abstract URI Identifier { get; }

        public abstract void Encode(OutputStream output, Indentation indenter);

        #endregion

        public abstract DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx);
    }
}