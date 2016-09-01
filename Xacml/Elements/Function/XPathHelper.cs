namespace Xacml.Elements.Function
{
    using Xacml.Elements.Context;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;

    public abstract class XPathHelper : Function
    {
        protected internal static XPath _XPath = XPathFactory.NewInstance().NewXPath();

        public virtual bool IsNodeListMatch(NodeList targetList, NodeList list)
        {
            for (int i = 0; i < targetList.Length; i++)
            {
                Node target = targetList.Item(i);
                for (int j = 0; j < list.Length; j++)
                {
                    Node node = list.Item(j);
                    if (this.IsNodeMatch(target, node))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual bool IsNodeMatch(Node target, Node node)
        {
            if (target.IsEqualNode(node))
            {
                return true;
            }
            else
            {
                NodeList children = target.ChildNodes;
                for (int i = 0; i < children.Length; i++)
                {
                    Node child = children.Item(i);
                    if (this.IsNodeMatch(child, node))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual NodeList EvaluateXPath(string xPathExpression, Node node, XPathNamespaceContext nsctx)
        {
            _XPath.NamespaceContext = nsctx;
            return _XPath.Evaluate(xPathExpression, node);
        }
    }
}