namespace Xacml.Elements.Policy.Proxy
{
    using Xacml.Types.Xml;

    public class MatchProxy : IPolicyElementFactoryProxy
    {
        #region IPolicyElementFactoryProxy Members

        public IElement GetInstance(Node node)
        {
            return Match.GetInstance(node);
        }

        public IElement GetInstance(string value)
        {
            return Match.GetInstance(value);
        }

        #endregion
    }
}