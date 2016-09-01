namespace Xacml.Elements.Policy
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Elements.Function;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class Match : IEvaluatable
    {
        public const string stringIdentifer = "Match";
        private readonly AttributeValue _attributeValue;
        private readonly IEvaluatable _evaluatable;
        private readonly AnyURIDataType _matchId;

        public Match(Node node)
        {
            Node matchidnode = node.Attributes.GetNamedItem("MatchId");
            if (matchidnode != null)
            {
                this._matchId = new AnyURIDataType(matchidnode.NodeValue);
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            this._evaluatable = null;
            this._attributeValue = null;
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Trim().Equals(AttributeValue.stringIdentifer))
                {
                    this._attributeValue = new AttributeValue(child);
                }
                else if (this._evaluatable == null && child.NodeName.Trim().Equals(AttributeDesignator.stringIdentifer))
                {
                    this._evaluatable = new AttributeDesignator(child);
                }
                else if (this._evaluatable == null && child.NodeName.Trim().Equals(AttributeSelector.stringIdentifer))
                {
                    this._evaluatable = new AttributeSelector(child);
                }
            }

            if (this._evaluatable == null || this._attributeValue == null)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IEvaluatable Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Match MatchId=\"" + this._matchId.Encode() + "\">");
            indenter.Down();
            this._attributeValue.Encode(output, indenter);
            this._evaluatable.Encode(output, indenter);
            indenter.Up();
            psout.PrintLine(indenter + "</Match>");
        }

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            var args = (BagDataType)this._evaluatable.Evaluate(ctx, SchemeID);
            IList children = args.Children;
            DataTypeValue ret = null;
            foreach (object o in children)
            {
                var arg = (DataTypeValue)o;
                var @params = new[] { this._attributeValue.DataTypeValue, arg };
                ret = FunctionFactory.Evaluate(this._matchId.Value, @params, ctx);
                if (BooleanDataType.True.Equals(ret))
                {
                    return ret;
                }
            }
            return BooleanDataType.False;
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new Match(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Match(NodeFactory.GetInstanceFromString(value));
        }
    }
}