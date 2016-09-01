namespace Xacml.Elements.Policy
{
    using System.Collections.Generic;
    using System.Linq;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Elements.Function;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class Apply : IEvaluatable
    {
        public const string stringIdentifer = "Apply";
        private readonly Description _description;
        private readonly List<IEvaluatable> _evaluatable;
        private readonly string _functionId;

        public Apply(Node node)
        {
            Node _FunctionIdNode = node.Attributes.GetNamedItem("FunctionId");
            if (_FunctionIdNode != null)
            {
                this._functionId = _FunctionIdNode.NodeValue;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            NodeList children = node.ChildNodes;
            this._description = null;
            this._evaluatable = new List<IEvaluatable>();
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(Description.stringIdentifer))
                {
                    this._description = (Description)PolicyElementFactory.GetInstance(child);
                }
                else if (child.NodeType == Node.ELEMENT_NODE)
                {
                    IElement e = PolicyElementFactory.GetInstance(child);
                    if (e is IEvaluatable)
                    {
                        this._evaluatable.Add((IEvaluatable)e);
                    }
                }
            }
        }

        #region IEvaluatable Members

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            IList<DataTypeValue> rets = new List<DataTypeValue>();
            foreach (IEvaluatable e in this._evaluatable)
            {
                rets.Add(e.Evaluate(ctx, SchemeID));
            }
            return FunctionFactory.Evaluate(this._functionId, rets.ToArray(), ctx);
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Apply FunctionId=\"" + this._functionId + "\">");
            indenter.Down();
            if (this._description != null)
            {
                this._description.Encode(output, indenter);
            }
            foreach (IEvaluatable e in this._evaluatable)
            {
                e.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</Apply>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new Apply(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Apply(NodeFactory.GetInstanceFromString(value));
        }
    }
}