namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class ObligationExpressionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return ObligationExpression.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return ObligationExpression.GetInstance(value);
        }

        #endregion
    }
}