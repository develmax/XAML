namespace Xacml.Elements.Function.Match
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class XPathNodeMatch : XPathHelper
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:3.0:function:xpath-node-match";
        internal const int paramsnum = 2;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        public override URI Identifier
        {
            get { throw new UnsupportedOperationException("Not supported yet."); }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            if (@params.Length == paramsnum && @params[0] is XpathExpressionDataType &&
                @params[1] is XpathExpressionDataType)
            {
                try
                {
                    NodeList ret1 = this.EvaluateXPath(
                        (@params[0]).Value, ctx.ContentRoot, ctx.Request.Content.XPathNamespaceContext);
                    NodeList ret2 = this.EvaluateXPath(
                        (@params[1]).Value, ctx.ContentRoot, ctx.Request.Content.XPathNamespaceContext);

                    if (this.IsNodeListMatch(ret1, ret2))
                    {
                        return BooleanDataType.True;
                    }
                    else
                    {
                        return BooleanDataType.False;
                    }
                }
                catch (Indeterminate ex)
                {
                    throw new IllegalExpressionEvaluationException(ex.Message);
                }
                catch (XPathExpressionException ex)
                {
                    throw new IllegalExpressionEvaluationException(ex.Message);
                }
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}