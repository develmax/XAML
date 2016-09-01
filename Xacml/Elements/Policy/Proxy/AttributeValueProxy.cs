namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AttributeValueProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AttributeValue.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AttributeValue.GetInstance(value);
        }

        #endregion
    }
}