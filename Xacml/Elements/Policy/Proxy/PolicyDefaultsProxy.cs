namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicyDefaultsProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicyDefaults.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicyDefaults.GetInstance(value);
        }

        #endregion
    }
}