namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class TargetProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Target.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Target.GetInstance(value);
        }

        #endregion
    }
}