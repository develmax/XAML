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

    public class AnyOf : IEvaluatable
    {
        public const string stringIdentifer = "AnyOf";
        private readonly List<AllOf> _anyofs;

        public AnyOf(Node node)
        {
            if (node.NodeName.Equals(stringIdentifer) == false)
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
            this._anyofs = new List<AllOf>();
            NodeList children = node.ChildNodes;
            for (int i = 0; i < children.Length; i++)
            {
                Node child = children.Item(i);
                if (child.NodeName.Equals(AllOf.stringIdentifer))
                {
                    this._anyofs.Add(new AllOf(child));
                }
            }
            if (this._anyofs.Count == 0)
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
            psout.PrintLine(indenter + "<AnyOf>");
            indenter.Down();
            foreach (object o in this._anyofs)
            {
                ((AllOf)o).Encode(output, indenter);
            }
            indenter.Up();
            psout.PrintLine(indenter + "</AnyOf>");
        }

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            foreach (object o in this._anyofs)
            {
                DataTypeValue ret = ((AllOf)o).Evaluate(ctx, SchemeID);
                if (ret.Equals(BooleanDataType.True))
                {
                    return ret;
                }
            }
            return BooleanDataType.False;
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new AnyOf(node);
        }

        public static IElement GetInstance(string value)
        {
            return new AnyOf(NodeFactory.GetInstanceFromString(value));
        }
    }
}