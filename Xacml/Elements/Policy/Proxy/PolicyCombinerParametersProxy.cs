namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicyCombinerParametersProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicyCombinerParameters.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicyCombinerParameters.GetInstance(value);
        }

        #endregion
    }
}