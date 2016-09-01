namespace Xacml.Elements.Function.Bag
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Types.Web;

    public class DnsNameIsIn : TypeIsIn
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:2.0:function:dnsName-is-in";
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        internal override string StringIdentifer
        {
            get { return stringIdentifer; }
        }

        public override URI Identifier
        {
            get { return URIID; }
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            return this.Evaluate(@params, ctx, typeof(DnsNameDataType));
        }
    }
}