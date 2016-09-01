namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AdviceExpressionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AdviceExpression.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AdviceExpression.GetInstance(value);
        }

        #endregion
    }
}