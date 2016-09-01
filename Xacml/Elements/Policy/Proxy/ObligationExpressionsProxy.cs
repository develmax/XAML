namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class ObligationExpressionsProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return ObligationExpressions.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return ObligationExpressions.GetInstance(value);
        }

        #endregion
    }
}