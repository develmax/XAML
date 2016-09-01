namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class RuleProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Rule.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Rule.GetInstance(value);
        }

        #endregion
    }
}