namespace Xacml.Elements.Policy
{
    using System.Collections.Generic;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class AllOf : IEvaluatable
    {
        public const string stringIdentifer = "AllOf";
        private readonly List<Match> _matchs;

        public AllOf(Node node)
        {
            this._matchs = new List<Match>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(Match.stringIdentifer))
                {
                    this._matchs.Add(new Match(child));
                }
            }
            if (this._matchs.Count == 0)
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
            psout.PrintLine(indenter + "<AllOf>");
            indenter.Down();
            foreach (object o in this._matchs)
            {
                ((Match)o).Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</AllOf>");
        }

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            foreach (object o in this._matchs)
            {
                DataTypeValue value = ((Match)o).Evaluate(ctx, SchemeID);
                if (value.Equals(BooleanDataType.False))
                {
                    return BooleanDataType.False;
                }
            }
            return BooleanDataType.True;
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new AllOf(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AllOf(NodeFactory.GetInstanceFromString(value));
        }
    }
}