namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AdviceExpressionsProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AdviceExpressions.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AdviceExpressions.GetInstance(value);
        }

        #endregion
    }
}