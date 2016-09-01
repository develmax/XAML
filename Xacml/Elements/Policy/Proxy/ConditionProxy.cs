namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class ConditionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Condition.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Condition.GetInstance(value);
        }

        #endregion
    }
}