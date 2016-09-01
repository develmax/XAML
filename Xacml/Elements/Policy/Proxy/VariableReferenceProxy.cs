namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class VariableReferenceProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return VariableReference.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return VariableReference.GetInstance(value);
        }

        #endregion
    }
}