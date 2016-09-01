namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;

    public interface IPolicyLanguageModel : IElement
    {
        string ElementId { get; }
        void Evaluate(EvaluationContext ctx, string SchemeID);

        VariableDefinition GetVariableDefinition(string variableID);
    }
}