namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicySetIdReferenceProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicySetIdReference.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicySetIdReference.GetInstance(value);
        }

        #endregion
    }
}