namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AnyOfProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AnyOf.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AnyOf.GetInstance(value);
        }

        #endregion
    }
}