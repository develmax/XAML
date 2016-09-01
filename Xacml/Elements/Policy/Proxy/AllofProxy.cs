namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AllofProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AllOf.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AllOf.GetInstance(value);
        }

        #endregion
    }
}