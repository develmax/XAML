namespace Xacml.Elements.Policy
{
    using System.Collections.Generic;

    using Xacml.Elements.Context;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class AdviceExpressions : IObligationsAndAdviceAvaluatable
    {
        public const string stringIdentifer = "AdviceExpressions";

        private readonly List<AdviceExpression> _adviceExpressions;

        public AdviceExpressions(Node node)
        {
            this._adviceExpressions = new List<AdviceExpression>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(AdviceExpression.stringIdentifer))
                {
                    this._adviceExpressions.Add((AdviceExpression)PolicyElementFactory.GetInstance(child));
                }
            }
            if (this._adviceExpressions.Count == 0)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IObligationsAndAdviceAvaluatable Members

        public IElement Evaluate(EvaluationContext ctx, string SchemeID, string effect)
        {
            var associatedAdvice = new AssociatedAdvice();
            foreach (AdviceExpression o in this._adviceExpressions)
            {
                var advice = (Advice)o.Evaluate(ctx, SchemeID, effect);
                if (advice != null)
                {
                    associatedAdvice.AddAdvice(advice);
                }
            }
            return associatedAdvice;
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<AdviceExpressions>");
            indenter.Down();
            foreach (AdviceExpression o in this._adviceExpressions)
            {
                o.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</AdviceExpressions>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new AdviceExpressions(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AdviceExpressions(NodeFactory.GetInstanceFromString(value));
        }
    }
}