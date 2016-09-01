namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class DescriptionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Description.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Description.GetInstance(value);
        }

        #endregion
    }
}