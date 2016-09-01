namespace Xacml.Elements.Function.Match
{
    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Exceptions;
    using Xacml.Types.Helpers;
    using Xacml.Types.Streams;
    using Xacml.Types.Web;
    using Xacml.Utils;

    public class Rfc822NameMatch : Function
    {
        public const string stringIdentifer = "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-match";
        internal const int paramsnum = 2;
        internal static readonly URI URIID = URI.Create(stringIdentifer);

        public override URI Identifier
        {
            get { return URIID; }
        }

        public override void Encode(OutputStream output, Indentation indenter)
        {
            throw new UnsupportedOperationException("Not supported yet.");
        }

        public override DataTypeValue Evaluate(DataTypeValue[] @params, EvaluationContext ctx)
        {
            if (@params.Length == paramsnum && (@params[0] is StringDataType) && (@params[1] is RFC822NameDataType))
            {
                string arg0 = (@params[0]).Value;
                string arg1 = (@params[1]).Value;
                string[] par1 = arg1.StringSplit("@", true);
                if (arg0.Contains("@"))
                {
                    string[] par0 = arg0.StringSplit("@", true);
                    if (par0[0].Equals(par1[0]) && par0[1].EqualsIgnoreCase(par1[1]))
                    {
                        return BooleanDataType.True;
                    }
                }
                else
                {
                    if (arg0.EqualsIgnoreCase(par1[1]))
                    {
                        return BooleanDataType.True;
                    }
                }
                return BooleanDataType.False;
            }
            throw new IllegalExpressionEvaluationException(stringIdentifer);
        }
    }
}