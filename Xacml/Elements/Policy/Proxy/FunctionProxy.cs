namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class FunctionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Function.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Function.GetInstance(value);
        }

        #endregion
    }
}