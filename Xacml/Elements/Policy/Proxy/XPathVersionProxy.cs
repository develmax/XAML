namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Elements.Context;
    using Xacml.Types.Xml;

    public class XPathVersionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return XPathVersion.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return XPathVersion.GetInstance(value);
        }

        #endregion
    }
}