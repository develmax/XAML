namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class VariableDefinitionProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return VariableDefinition.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return VariableDefinition.GetInstance(value);
        }

        #endregion
    }
}