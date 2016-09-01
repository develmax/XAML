namespace Xacml.Elements.Function.Bag
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Types.Web;

    public class IpAddressOneAndOnly : OneAndOnly
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:2.0:function:ipAddress-one-and-only";
        internal const int paramsnum = 1;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        public override URI Identifier
        {
            get { return URIID; }
        }

        internal override int Paramsnum
        {
            get { return paramsnum; }
        }

        internal override string StringIdentifer
        {
            get { return stringIdentifer; }
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            return this.Evaluate(@params, ctx, typeof(IpAddressDataType));
        }
    }
}