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

    public class ObligationExpression : IObligationsAndAdviceAvaluatable
    {
        public const string stringIdentifer = "ObligationExpression";
        private readonly IList<AttributeAssignmentExpression> _attributeAssignmentExpressions;
        private readonly string _fulfillOn;
        private readonly string _obligationId;

        private ObligationExpression(Node node)
        {
            Node ObligationIdNode = node.Attributes.GetNamedItem("ObligationId");
            if (ObligationIdNode != null)
            {
                this._obligationId = ObligationIdNode.NodeValue;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            Node FulfillOnNode = node.Attributes.GetNamedItem("FulfillOn");
            if (FulfillOnNode != null)
            {
                this._fulfillOn = FulfillOnNode.NodeValue;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            this._attributeAssignmentExpressions = new List<AttributeAssignmentExpression>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(AttributeAssignmentExpression.stringIdentifer))
                {
                    this._attributeAssignmentExpressions.Add(
                        (AttributeAssignmentExpression)PolicyElementFactory.GetInstance(child));
                }
            }
        }

        #region IObligationsAndAdviceAvaluatable Members

        public IElement Evaluate(EvaluationContext ctx, string SchemeID, string effect)
        {
            if (this._fulfillOn.Equals(effect))
            {
                var obligation = new Obligation(this._obligationId);
                foreach (AttributeAssignmentExpression attr in this._attributeAssignmentExpressions)
                {
                    obligation.AddAttributeAssignment((AttributeAssignment)attr.Evaluate(ctx, SchemeID, effect));
                }
                return obligation;
            }
            else
            {
                return null;
            }
        }

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<ObligationExpression ObligationId=\"" + this._obligationId + "\"");
            if (this._fulfillOn != null)
            {
                psout.PrintLine(indenter + " FulfillOn=\"" + this._fulfillOn + "\"");
            }
            psout.PrintLine(indenter + ">");
            indenter.Down();
            foreach (AttributeAssignmentExpression attr in this._attributeAssignmentExpressions)
            {
                attr.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</ObligationExpression>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new ObligationExpression(node);
        }

        public static IElement GetInstance(string value)
        {
            return new ObligationExpression(NodeFactory.GetInstanceFromString(value));
        }
    }
}