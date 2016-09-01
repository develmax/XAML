namespace Xacml.Elements.Policy
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.Policy.Proxy;
    using Xacml.Exceptions;
    using Xacml.Types.Xml;

    public class PolicyElementFactory
    {
        private static PolicyElementFactory factoryInstance;
        private static readonly Hashtable _elements = new Hashtable();

        private static void InitializeSupportedElements()
        {
            _elements.Add(PolicySet.stringIdentifer, new PolicySetProxy());
            _elements.Add(Policy.stringIdentifer, new PolicyProxy());
            _elements.Add(Rule.stringIdentifer, new RuleProxy());
            _elements.Add(AttributeDesignator.stringIdentifer, new AttributeDesignatorProxy());
            _elements.Add(Target.stringIdentifer, new TargetProxy());
            _elements.Add(AllOf.stringIdentifer, new AllofProxy());
            _elements.Add(AnyOf.stringIdentifer, new AnyOfProxy());
            _elements.Add(AttributeValue.stringIdentifer, new AttributeValueProxy());
            _elements.Add(Description.stringIdentifer, new DescriptionProxy());
            _elements.Add(Match.stringIdentifer, new MatchProxy());
            _elements.Add(PolicyDefaults.stringIdentifer, new PolicyDefaultsProxy());
            _elements.Add(PolicyIssuer.stringIdentifer, new PolicyIssuerProxy());
            _elements.Add(PolicySetDefaults.stringIdentifer, new PolicySetDefaultsProxy());
            _elements.Add(XPathVersion.Identifer, new XPathVersionProxy());
            _elements.Add(CombinerParameters.stringIdentifer, new CombinerParametersProxy());
            _elements.Add(CombinerParameter.stringIdentifer, new CombinerParameterProxy());
            _elements.Add(VariableDefinition.stringIdentifer, new VariableDefinitionProxy());
            _elements.Add(Apply.stringIdentifer, new ApplyProxy());
            _elements.Add(Condition.stringIdentifer, new ConditionProxy());
            _elements.Add(VariableReference.stringIdentifer, new VariableReferenceProxy());
            _elements.Add(AttributeSelector.stringIdentifer, new AttributeSelectorProxy());
            _elements.Add(AttributeAssignmentExpression.stringIdentifer, new AttributeAssignmentExpressionProxy());
            _elements.Add(ObligationExpression.stringIdentifer, new ObligationExpressionProxy());
            _elements.Add(ObligationExpressions.stringIdentifer, new ObligationExpressionsProxy());
            _elements.Add(PolicyIdReference.Identifer, new PolicyIdReferenceProxy());
            _elements.Add(AdviceExpressions.stringIdentifer, new AdviceExpressionsProxy());
            _elements.Add(AdviceExpression.stringIdentifer, new AdviceExpressionProxy());
            _elements.Add(PolicySetIdReference.Identifer, new PolicySetIdReferenceProxy());
            _elements.Add(PolicyCombinerParameters.stringIdentifer, new PolicyCombinerParametersProxy());
            _elements.Add(PolicySetCombinerParameters.stringIdentifer, new PolicySetCombinerParametersProxy());
            _elements.Add(Function.stringIdentifer, new FunctionProxy());
            _elements.Add(RuleCombinerParameters.stringIdentifer, new RuleCombinerParametersProxy());
        }

        public static IElement GetInstance(Node node)
        {
            Init();
            string name = node.NodeName;
            if (_elements.ContainsKey(name))
            {
                return ((IPolicyElementFactoryProxy)_elements[name]).GetInstance(node);
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        public static IElement GetInstance(string name)
        {
            Init();
            if (_elements.ContainsKey(name))
            {
                return ((IPolicyElementFactoryProxy)_elements[name]).GetInstance(name);
            }
            throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        public static void Init()
        {
            if (factoryInstance == null)
            {
                InitializeSupportedElements();
                factoryInstance = new PolicyElementFactory();
            }
        }
    }
}