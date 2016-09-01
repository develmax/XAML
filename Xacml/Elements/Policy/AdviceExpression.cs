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

    public class AdviceExpression : IObligationsAndAdviceAvaluatable
    {
        public const string stringIdentifer = "AdviceExpression";
        private readonly string _adviceId;
        private readonly string _appliesTo;
        private readonly List<AttributeAssignmentExpression> _attributeAssignmentExpressions;

        private AdviceExpression(Node node)
        {
            Node AdviceIdNode = node.Attributes.GetNamedItem("AdviceId");
            if (AdviceIdNode != null)
            {
                this._adviceId = AdviceIdNode.NodeValue;
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }

            Node AppliesToNode = node.Attributes.GetNamedItem("AppliesTo");
            if (AdviceIdNode != null)
            {
                this._appliesTo = AppliesToNode.NodeValue;
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
            if (this._appliesTo.Equals(effect))
            {
                var advice = new Advice(this._adviceId);
                foreach (AttributeAssignmentExpression attr in this._attributeAssignmentExpressions)
                {
                    advice.addAttributeAssignment((AttributeAssignment)attr.Evaluate(ctx, SchemeID, effect));
                }
                return advice;
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
            psout.PrintLine(
                indenter + "<AdviceExpression AdviceId=\"" + this._adviceId + "\" AppliesTo=\"" + this._appliesTo +
                "\">");
            indenter.Down();
            foreach (AttributeAssignmentExpression o in this._attributeAssignmentExpressions)
            {
                o.Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</AdviceExpression>");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new AdviceExpression(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AdviceExpression(NodeFactory.GetInstanceFromString(value));
        }
    }
}