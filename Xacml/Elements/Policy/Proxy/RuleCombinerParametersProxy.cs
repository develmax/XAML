namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class RuleCombinerParametersProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return RuleCombinerParameters.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return RuleCombinerParameters.GetInstance(value);
        }

        #endregion
    }
}