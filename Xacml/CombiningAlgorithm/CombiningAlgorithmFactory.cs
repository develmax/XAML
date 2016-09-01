namespace Xacml.CombiningAlgorithm
{
    using System.Collections.Generic;

    using Xacml.CombiningAlgorithm.V10;
    using Xacml.CombiningAlgorithm.V30;
    using Xacml.Elements.Context;
    using Xacml.Elements.Policy;

    public class CombiningAlgorithmFactory
    {
        private static CombiningAlgorithmFactory Instance;

        private static readonly Dictionary<string, ICombiningAlgorithm> _elements =
            new Dictionary<string, ICombiningAlgorithm>();

        private static void InitializeSupportedElements()
        {
            _elements.Add(
                CombingAlgorithmPolicyPermitOverrides30.Identifer, new CombingAlgorithmPolicyPermitOverrides30());
            _elements.Add(CombingAlgorithmRulePermitOverrides30.Identifer, new CombingAlgorithmRulePermitOverrides30());
            _elements.Add(CombingAlgorithmRuleDenyOverrides30.Identifer, new CombingAlgorithmRuleDenyOverrides30());
            _elements.Add(CombingAlgorithmPolicyDenyOverrides30.Identifer, new CombingAlgorithmPolicyDenyOverrides30());

            _elements.Add(CombingAlgorithmRuleDenyOverrides10.Identifer, new CombingAlgorithmRuleDenyOverrides10());
            _elements.Add(CombingAlgorithmPolicyDenyOverrides10.Identifer, new CombingAlgorithmPolicyDenyOverrides10());

            _elements.Add(CombingAlgorithmRulePermitOverrides10.Identifer, new CombingAlgorithmRulePermitOverrides10());
            _elements.Add(
                CombingAlgorithmPolicyPermitOverrides10.Identifer, new CombingAlgorithmPolicyPermitOverrides10());

            _elements.Add(CombingAlgorithmRuleFirstApplicable10.Identifer, new CombingAlgorithmRuleFirstApplicable10());
            _elements.Add(
                CombingAlgorithmPolicyFirstApplicable10.Identifer, new CombingAlgorithmPolicyFirstApplicable10());

            _elements.Add(
                CombingAlgorithmPolicyOnlyOneApplicable10.Identifer, new CombingAlgorithmPolicyOnlyOneApplicable10());

            _elements.Add(
                CombingAlgorithmPolicyOrderedDenyOverrides30.Identifer,
                new CombingAlgorithmPolicyOrderedDenyOverrides30());
            _elements.Add(
                CombingAlgorithmRuleOrderedDenyOverrides30.Identifer, new CombingAlgorithmRuleOrderedDenyOverrides30());

            _elements.Add(
                CombingAlgorithmPolicyOrderedPermitOverrides30.Identifer,
                new CombingAlgorithmPolicyOrderedPermitOverrides30());
            _elements.Add(
                CombingAlgorithmRuleOrderedPermitOverrides30.Identifer,
                new CombingAlgorithmRuleOrderedPermitOverrides30());

            _elements.Add(
                CombingAlgorithmPolicyDenyUnlessPermit30.Identifer, new CombingAlgorithmPolicyDenyUnlessPermit30());
            _elements.Add(
                CombingAlgorithmRuleDenyUnlessPermit30.Identifer, new CombingAlgorithmRuleDenyUnlessPermit30());

            _elements.Add(
                CombingAlgorithmPolicyPermitUnlessDeny30.Identifer, new CombingAlgorithmPolicyPermitUnlessDeny30());
            _elements.Add(
                CombingAlgorithmRulePermitUnlessDeny30.Identifer, new CombingAlgorithmRulePermitUnlessDeny30());
        }

        public static void Evaluate(string name, EvaluationContext ctx, IPolicyLanguageModel[] evals, string ID)
        {
            Init();

            if (_elements.ContainsKey(name))
            {
                ICombiningAlgorithm func = _elements[name];
                func.Evaluate(ctx, evals, ID);
                return;
            }

            ctx.GetResult(ID).Decision = Decision.Indeterminate;
        }

        public static void Init()
        {
            if (Instance == null)
            {
                Instance = new CombiningAlgorithmFactory();
                InitializeSupportedElements();
            }
        }
    }
}