namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicySetDefaultsProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicySetDefaults.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicySetDefaults.GetInstance(value);
        }

        #endregion
    }
}