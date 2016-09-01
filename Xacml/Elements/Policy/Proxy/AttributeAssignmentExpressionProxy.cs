namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class AttributeAssignmentExpressionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return AttributeAssignmentExpression.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return AttributeAssignmentExpression.GetInstance(value);
        }

        #endregion
    }
}