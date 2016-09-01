namespace Xacml.Elements.Policy
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class Function : IEvaluatable
    {
        public const string stringIdentifer = "Function";
        private readonly string _functionId;

        public Function(Node node)
        {
            Node _FunctionIdnode = node.Attributes.GetNamedItem("FunctionId");
            if (_FunctionIdnode != null)
            {
                this._functionId = _FunctionIdnode.NodeValue.Trim();
            }
            else
            {
                throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
            }
        }

        #region IEvaluatable Members

        public virtual DataTypeValue Evaluate(EvaluationContext ctx, string SchemeID)
        {
            return DataTypeFactory.Instance.CreateValue(StringDataType.Identifer, this._functionId);
        }

        public virtual URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public virtual void Encode(OutputStream output, Indentation indenter)
        {
            var psout = new PrintStream(output);
            psout.PrintLine(indenter + "<Function FunctionId=\"" + this._functionId + "\" />");
        }

        #endregion

        public static IElement GetInstance(Node node)
        {
            return new Function(node);
        }

        public static IElement GetInstance(string value)
        {
            return new Function(NodeFactory.GetInstanceFromString(value));
        }
    }
}