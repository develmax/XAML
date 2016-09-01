namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class CombinerParameterProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return CombinerParameter.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return CombinerParameter.GetInstance(value);
        }

        #endregion
    }
}