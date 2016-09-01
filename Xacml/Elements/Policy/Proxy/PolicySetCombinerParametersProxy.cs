namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicySetCombinerParametersProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicySetCombinerParameters.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicySetCombinerParameters.GetInstance(value);
        }

        #endregion
    }
}