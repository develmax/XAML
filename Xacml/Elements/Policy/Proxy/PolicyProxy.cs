namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class PolicyProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Policy.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Policy.GetInstance(value);
        }

        #endregion
    }
}