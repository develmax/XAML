namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;

    public interface IObligationsAndAdviceAvaluatable : IElement
    {
        IElement Evaluate(EvaluationContext ctx, string SchemeID, string effect);
    }
}