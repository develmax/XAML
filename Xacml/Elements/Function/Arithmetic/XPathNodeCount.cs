namespace Xacml.Elements.Function.Arithmetic
{
    using System;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Types.Xml;
    using Xacml.Utils;

    public class XPathNodeCount : XPathHelper
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:3.0:function:xpath-node-count";
        internal const int paramsnum = 1;
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
            if (@params.Length == paramsnum && @params[0] is XpathExpressionDataType)
            {
                try
                {
                    NodeList ret1 = this.EvaluateXPath(
                        (@params[0]).Value, ctx.ContentRoot, ctx.Request.Content.XPathNamespaceContext);
                    int count = ret1.Length;
                    return new IntegerDataType(Convert.ToString(count));
                }
                catch (Indeterminate)
                {
                }
                catch (XPathExpressionException)
                {
                }
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}