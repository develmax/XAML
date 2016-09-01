namespace Xacml.Types.Xml
{
    using Xacml.Elements.Context;

    public class XPath
    {
        public XPathNamespaceContext NamespaceContext;

        public NodeList Evaluate(string xPathExpression, Node node)
        {
            return new NodeList(node.Value.SelectNodes(xPathExpression, this.NamespaceContext.Manager));
        }
    }
}