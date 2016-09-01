namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;

    public interface IEvaluatable : IElement
    {
        DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID);
    }
}