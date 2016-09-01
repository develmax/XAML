namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AttributeDesignatorProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AttributeDesignator.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AttributeDesignator.GetInstance(value);
        }

        #endregion
    }
}