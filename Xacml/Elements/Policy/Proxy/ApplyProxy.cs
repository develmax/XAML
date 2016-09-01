namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class ApplyProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Apply.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Apply.GetInstance(value);
        }

        #endregion
    }
}