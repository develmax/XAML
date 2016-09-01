namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicyIssuerProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicyIssuer.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicyIssuer.GetInstance(value);
        }

        #endregion
    }
}