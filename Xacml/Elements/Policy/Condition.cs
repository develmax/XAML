namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class Condition : IEvaluatable
    {
        public const string stringIdentifer = "Condition";
        private readonly IEvaluatable _evaluatable;

        public Condition(Node node)
        {
            this._evaluatable = null;
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeType == Node.ELEMENT_NODE)
                {
                    IElement e = PolicyElementFactory.GetInstance(children.Item(i));
                    if (e != null && e is IEvaluatable)
                    {
                        this._evaluatable = (IEvaluatable)e;
                        break;
                    }
                }
            }
            if (this._evaluatable == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IEvaluatable Members

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            return this._evaluatable.Evaluate(ctx, SchemeID);
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Condition>");
            indenter.Down();
            this._evaluatable.Encode(output, indenter);
            indenter.Up();
            psout.PrintLine(indenter + "</Condition>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new Condition(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Condition(NodeFactory.GetInstanceFromString(value));
        }
    }
}