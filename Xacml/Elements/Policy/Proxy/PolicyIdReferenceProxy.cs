namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicyIdReferenceProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return PolicyIdReference.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return PolicyIdReference.GetInstance(value);
        }

        #endregion
    }
}