namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AttributeSelectorProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AttributeSelector.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AttributeSelector.GetInstance(value);
        }

        #endregion
    }
}