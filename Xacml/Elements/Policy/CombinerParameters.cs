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

    public class CombinerParameters : IPolicyLanguageModel
    {
        public const string stringIdentifer = "CombinerParameters";
        private readonly List<CombinerParameter> _combinerParameters;

        public CombinerParameters(Node node)
        {
            this._combinerParameters = new List<CombinerParameter>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(CombinerParameter.stringIdentifer))
                {
                    this._combinerParameters.Add((CombinerParameter)PolicyElementFactory.GetInstance(child));
                }
            }
        }

        #region IPolicyLanguageModel Members

        public URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            if (this._combinerParameters.Count == 0)
            {
                psout.PrintLine(indenter + "<CombinerParameters />");
            }
            else
            {
                psout.PrintLine(indenter + "<CombinerParameters>");
                indenter.Down();
                foreach (object o in this._combinerParameters)
                {
                    ((CombinerParameter)o).Encode(output, indenter);
                }
                indenter.Up();
                psout.PrintLine(indenter + "</CombinerParameters>");
            }
        }

        public void Evaluate(EvaluationContext ctx, string SchemeID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public string ElementId
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public VariableDefinition GetVariableDefinition(string variableID)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new CombinerParameters(node);
        }

        public static IElement GetInstance(string value)
        {
            return new CombinerParameters(NodeFactory.GetInstanceFromString(value));
        }
    }
}