namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class CombinerParametersProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return CombinerParameters.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return CombinerParameters.GetInstance(value);
        }

        #endregion
    }
}