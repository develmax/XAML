namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicySetProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicySet.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicySet.GetInstance(value);
        }

        #endregion
    }
}