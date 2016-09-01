namespace Xacml.Elements.Policy
{
    using System.Collections;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class Target : IEvaluatable
    {
        public const string stringIdentifer = "Target";
        private readonly IList _anyofs;

        public Target(Node node)
        {
            NodeList Children = node.ChildNodes;
            this._anyofs = new ArrayList();
            for (int i = 0; i < Children.Length; i++)
            {
                Node child = Children.Item(i);
                if (child.NodeName.Equals(AnyOf.stringIdentifer))
                {
                    this._anyofs.Add(PolicyElementFactory.GetInstance(Children.Item(i)));
                }
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
            if (this._anyofs.Count == 0)
            {
                psout.PrintLine(indenter + "<Target/>");
            }
            else
            {
                psout.PrintLine(indenter + "<Target>");
                indenter.Down();
                foreach (object o in this._anyofs)
                {
                    ((AnyOf)o).Encode(output, indenter);
                }
                indenter.Up();
                psout.PrintLine(indenter + "</Target>");
            }
        }

        public DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            foreach (object o in this._anyofs)
            {
                DataTypeValue ret = ((AnyOf)o).Evaluate(ctx, SchemeID);
                if (ret.Equals(BooleanDataType.False))
                {
                    return ret;
                }
            }
            return BooleanDataType.True;
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new Target(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Target(NodeFactory.GetInstanceFromString(value));
        }
    }
}