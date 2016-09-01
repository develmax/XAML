namespace Xacml.Elements.Function
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Elements.Policy;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml.Factories;
    using Xacml.Utils;

    public class AccessPermitted : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:3.0:function:access-permitted";
        private const int paramsnum = 2;
        private static readonly URI URIID = URI.Create(stringIdentifer);

        public override URI Identifier
        {
            get { return URIID; }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public override DataTypeValue Evaluate(DataTypeValue[] _params, EvaluationContext ctx)
        {
            int size = _params.Length;
            if (size == paramsnum && _params[0] is AnyURIDataType && _params[1] is StringDataType)
            {
                try
                {
                    Attributes _Attributes = Attributes.GetInstance(NodeFactory.GetInstanceFromString(_params[1].Value));
                    if (_params[0].Equals(_Attributes.Category) == false)
                    {
                        throw new IllegalExpressionEvaluationException(Indeterminate.IndeterminateSyntaxError);
                    }
                    var newctx = new EvaluationContext(ctx);
                    newctx.Request.replaceAttributes(_Attributes);
                    var ps = new PolicySchema(newctx.RootPolicyId);
                    ps.evluate(newctx);
                    if (newctx.GetResult(ps.RootElementID).Decision.IsPermit)
                    {
                        return BooleanDataType.True;
                    }
                    if (newctx.GetResult(ps.RootElementID).Decision.IsDeny)
                    {
                        return BooleanDataType.False;
                    }
                }
                catch (Indeterminate ex)
                {
                    throw new IllegalExpressionEvaluationException(ex.Message);
                }
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}