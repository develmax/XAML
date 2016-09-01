namespace Xacml.Elements.Policy
{
    using Xacml.Types.Xml;

    public interface IPolicyElementFactoryProxy
    {
        IElement GetInstance(Node node);
        IElement GetInstance(string value);
    }
}