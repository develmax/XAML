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

    public class ObligationExpressions : IObligationsAndAdviceAvaluatable
    {
        internal const string stringIdentifer = "ObligationExpressions";
        private readonly IList<ObligationExpression> _obligationExpressions;

        private ObligationExpressions(Node node)
        {
            this._obligationExpressions = new List<ObligationExpression>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(ObligationExpression.stringIdentifer))
                {
                    this._obligationExpressions.Add((ObligationExpression)PolicyElementFactory.GetInstance(child));
                }
            }
            if (this._obligationExpressions.Count == 0) throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
        }

        #region IObligationsAndAdviceAvaluatable Members

        public IElement Evaluate(EvaluationContext ctx, string SchemeID, string effect)
        {
            var obligations = new Obligations();
            foreach (ObligationExpression o in this._obligationExpressions)
            {
                var obligation = (Obligation)o.Evaluate(ctx, SchemeID, effect);
                if (obligation != null)
                {
                    obligations.addObligation(obligation);
                }
            }
            return obligations;
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<ObligationExpressions>");
            indenter.Down();
            foreach (ObligationExpression o in this._obligationExpressions)
            {
                o.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</ObligationExpressions>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new ObligationExpressions(node);
        }

        public static IElement GetInstance(string value)
        {
            return new ObligationExpressions(NodeFactory.GetInstanceFromString(value));
        }
    }
}