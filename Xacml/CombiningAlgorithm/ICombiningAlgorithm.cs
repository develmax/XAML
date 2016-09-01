namespace Xacml.CombiningAlgorithm
{
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public interface ICombiningAlgorithm
    {
        void Evaluate(EvaluationContext ctx, IPolicyLanguageModel[] evals, string ID);
    }
}