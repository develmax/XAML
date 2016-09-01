namespace Xacml.Elements.Function
{
    using System.Collections.Generic;

    using Xacml.Elements.Context;
    using Xacml.Elements.DataType;
    using Xacml.Elements.Function.Arithmetic;
    using Xacml.Elements.Function.Bag;
    using Xacml.Elements.Function.Equal;
    using Xacml.Elements.Function.Logic;
    using Xacml.Elements.Function.Match;
    using Xacml.Elements.Function.Set;
    using Xacml.Elements.Function.String;
    using Xacml.Exceptions;

    public class FunctionFactory
    {
        private static FunctionFactory _factoryInstance;

        private static readonly Dictionary<string, Function> _elements =
            new Dictionary<string, Function>();

        private static void InitializeSupportedElements()
        {
            //Equality predicates
            _elements.Add(StringEqual.stringIdentifer, new StringEqual());
            _elements.Add(BooleanEqual.stringIdentifer, new BooleanEqual());
            _elements.Add(IntegerEqual.stringIdentifer, new IntegerEqual());
            _elements.Add(DoubleEqual.stringIdentifer, new DoubleEqual());
            _elements.Add(DateEqual.stringIdentifer, new DateEqual());
            _elements.Add(TimeEqual.stringIdentifer, new TimeEqual());
            _elements.Add(DateTimeEqual.stringIdentifer, new DateTimeEqual());
            _elements.Add(DayTimeDurationEqual.stringIdentifer, new DayTimeDurationEqual());
            _elements.Add(StringEqualIgnoreCase.stringIdentifer, new StringEqualIgnoreCase());
            _elements.Add(AnyURIEqual.stringIdentifer, new AnyURIEqual());
            _elements.Add(X500NameEqual.stringIdentifer, new X500NameEqual());
            _elements.Add(Rfc822NameEqual.stringIdentifer, new Rfc822NameEqual());
            _elements.Add(HexBinaryEqual.stringIdentifer, new HexBinaryEqual());
            _elements.Add(Base64BinaryEqual.stringIdentifer, new Base64BinaryEqual());

            _elements.Add(IntegerAdd.stringIdentifer, new IntegerAdd());
            _elements.Add(DoubleAdd.stringIdentifer, new DoubleAdd());
            _elements.Add(IntegerSubtract.stringIdentifer, new IntegerSubtract());
            _elements.Add(DoubleSubtract.stringIdentifer, new DoubleSubtract());
            _elements.Add(IntegerMultiply.stringIdentifer, new IntegerMultiply());
            _elements.Add(DoubleMultiply.stringIdentifer, new DoubleMultiply());
            _elements.Add(IntegerDivide.stringIdentifer, new IntegerDivide());
            _elements.Add(DoubleDivide.stringIdentifer, new DoubleDivide());
            _elements.Add(IntegerMod.stringIdentifer, new IntegerMod());
            _elements.Add(IntegerAbs.stringIdentifer, new IntegerAbs());
            _elements.Add(DoubleAbs.stringIdentifer, new DoubleAbs());
            _elements.Add(Round.stringIdentifer, new Round());
            _elements.Add(Floor.stringIdentifer, new Floor());

            _elements.Add(StringNormalizeSpace.stringIdentifer, new StringNormalizeSpace());
            _elements.Add(StringNormalizeToLowercase.stringIdentifer, new StringNormalizeToLowercase());
            _elements.Add(DoubleToInteger.stringIdentifer, new DoubleToInteger());
            _elements.Add(IntegerToDouble.stringIdentifer, new IntegerToDouble());

            _elements.Add(Or.stringIdentifer, new Or());
            _elements.Add(And.stringIdentifer, new And());
            _elements.Add(NOf.stringIdentifer, new NOf());
            _elements.Add(Not.stringIdentifer, new Not());
            _elements.Add(IntegerGreaterThan.stringIdentifer, new IntegerGreaterThan());
            _elements.Add(IntegerGreaterThanOrEqual.stringIdentifer, new IntegerGreaterThanOrEqual());
            _elements.Add(IntegerLessThan.stringIdentifer, new IntegerLessThan());
            _elements.Add(IntegerLessThanOrEqual.stringIdentifer, new IntegerLessThanOrEqual());
            _elements.Add(DoubleGreaterThan.stringIdentifer, new DoubleGreaterThan());
            _elements.Add(DoubleGreaterThanOrEqual.stringIdentifer, new DoubleGreaterThanOrEqual());
            _elements.Add(DoubleLessThan.stringIdentifer, new DoubleLessThan());
            _elements.Add(DoubleLessThanOrEqual.stringIdentifer, new DoubleLessThanOrEqual());

            _elements.Add(DateTimeAddDayTimeDuration.stringIdentifer, new DateTimeAddDayTimeDuration());
            _elements.Add(DateTimeAddYearMonthDuration.stringIdentifer, new DateTimeAddYearMonthDuration());
            _elements.Add(DateTimeSubtractDayTimeDuration.stringIdentifer, new DateTimeSubtractDayTimeDuration());
            _elements.Add(DateTimeSubtractYearMonthDuration.stringIdentifer, new DateTimeSubtractYearMonthDuration());
            _elements.Add(DateAddYearMonthDuration.stringIdentifer, new DateAddYearMonthDuration());
            _elements.Add(DateSubtractYearMonthDuration.stringIdentifer, new DateSubtractYearMonthDuration());

            _elements.Add(StringGreaterThan.stringIdentifer, new StringGreaterThan());
            _elements.Add(StringGreaterThanOrEqual.stringIdentifer, new StringGreaterThanOrEqual());
            _elements.Add(StringLessThan.stringIdentifer, new StringLessThan());
            _elements.Add(StringLessThanOrEqual.stringIdentifer, new StringLessThanOrEqual());

            _elements.Add(TimeGreaterThan.stringIdentifer, new TimeGreaterThan());
            _elements.Add(TimeGreaterThanOrEqual.stringIdentifer, new TimeGreaterThanOrEqual());
            _elements.Add(TimeLessThan.stringIdentifer, new TimeLessThan());
            _elements.Add(TimeLessThanOrEqual.stringIdentifer, new TimeLessThanOrEqual());
            _elements.Add(TimeInRange.stringIdentifer, new TimeInRange());

            _elements.Add(DateTimeGreaterThan.stringIdentifer, new DateTimeGreaterThan());
            _elements.Add(DateTimeGreaterThanOrEqual.stringIdentifer, new DateTimeGreaterThanOrEqual());
            _elements.Add(DateTimeLessThan.stringIdentifer, new DateTimeLessThan());
            _elements.Add(DateTimeLessThanOrEqual.stringIdentifer, new DateTimeLessThanOrEqual());

            _elements.Add(DateGreaterThan.stringIdentifer, new DateGreaterThan());
            _elements.Add(DateGreaterThanOrEqual.stringIdentifer, new DateGreaterThanOrEqual());
            _elements.Add(DateLessThan.stringIdentifer, new DateLessThan());
            _elements.Add(DateLessThanOrEqual.stringIdentifer, new DateLessThanOrEqual());

            _elements.Add(StringOneAndOnly.stringIdentifer, new StringOneAndOnly());
            _elements.Add(StringBagSize.stringIdentifer, new StringBagSize());
            _elements.Add(StringIsIn.stringIdentifer, new StringIsIn());
            _elements.Add(StringBag.stringIdentifer, new StringBag());

            _elements.Add(BooleanOneAndOnly.stringIdentifer, new BooleanOneAndOnly());
            _elements.Add(BooleanBagSize.stringIdentifer, new BooleanBagSize());
            _elements.Add(BooleanIsIn.stringIdentifer, new BooleanIsIn());
            _elements.Add(BooleanBag.stringIdentifer, new BooleanBag());

            _elements.Add(IntegerOneAndOnly.stringIdentifer, new IntegerOneAndOnly());
            _elements.Add(IntegerBagSize.stringIdentifer, new IntegerBagSize());
            _elements.Add(IntegerIsIn.stringIdentifer, new IntegerIsIn());
            _elements.Add(IntegerBag.stringIdentifer, new IntegerBag());

            _elements.Add(DoubleOneAndOnly.stringIdentifer, new DoubleOneAndOnly());
            _elements.Add(DoubleBagSize.stringIdentifer, new DoubleBagSize());
            _elements.Add(DoubleIsIn.stringIdentifer, new DoubleIsIn());
            _elements.Add(DoubleBag.stringIdentifer, new DoubleBag());

            _elements.Add(TimeOneAndOnly.stringIdentifer, new TimeOneAndOnly());
            _elements.Add(TimeBagSize.stringIdentifer, new TimeBagSize());
            _elements.Add(TimeIsIn.stringIdentifer, new TimeIsIn());
            _elements.Add(TimeBag.stringIdentifer, new TimeBag());

            _elements.Add(DateOneAndOnly.stringIdentifer, new DateOneAndOnly());
            _elements.Add(DateBagSize.stringIdentifer, new DateBagSize());
            _elements.Add(DateIsIn.stringIdentifer, new DateIsIn());
            _elements.Add(DateBag.stringIdentifer, new DateBag());

            _elements.Add(DateTimeOneAndOnly.stringIdentifer, new DateTimeOneAndOnly());
            _elements.Add(DateTimeBagSize.stringIdentifer, new DateTimeBagSize());
            _elements.Add(DateTimeIsIn.stringIdentifer, new DateTimeIsIn());
            _elements.Add(DateTimeBag.stringIdentifer, new DateTimeBag());

            _elements.Add(AnyURIOneAndOnly.stringIdentifer, new AnyURIOneAndOnly());
            _elements.Add(AnyURIBagSize.stringIdentifer, new AnyURIBagSize());
            _elements.Add(AnyURIIsIn.stringIdentifer, new AnyURIIsIn());
            _elements.Add(AnyURIBag.stringIdentifer, new AnyURIBag());

            _elements.Add(HexBinaryOneAndOnly.stringIdentifer, new HexBinaryOneAndOnly());
            _elements.Add(HexBinaryBagSize.stringIdentifer, new HexBinaryBagSize());
            _elements.Add(HexBinaryIsIn.stringIdentifer, new HexBinaryIsIn());
            _elements.Add(HexBinaryBag.stringIdentifer, new HexBinaryBag());

            _elements.Add(Base64BinaryOneAndOnly.stringIdentifer, new Base64BinaryOneAndOnly());
            _elements.Add(Base64BinaryBagSize.stringIdentifer, new Base64BinaryBagSize());
            _elements.Add(Base64BinaryIsIn.stringIdentifer, new Base64BinaryIsIn());
            _elements.Add(Base64BinaryBag.stringIdentifer, new Base64BinaryBag());

            _elements.Add(DayTimeDurationOneAndOnly.stringIdentifer, new DayTimeDurationOneAndOnly());
            _elements.Add(DayTimeDurationBagSize.stringIdentifer, new DayTimeDurationBagSize());
            _elements.Add(DayTimeDurationIsIn.stringIdentifer, new DayTimeDurationIsIn());
            _elements.Add(DayTimeDurationBag.stringIdentifer, new DayTimeDurationBag());

            _elements.Add(YearMonthDurationOneAndOnly.stringIdentifer, new YearMonthDurationOneAndOnly());
            _elements.Add(YearMonthDurationBagSize.stringIdentifer, new YearMonthDurationBagSize());
            _elements.Add(YearMonthDurationIsIn.stringIdentifer, new YearMonthDurationIsIn());
            _elements.Add(YearMonthDurationBag.stringIdentifer, new YearMonthDurationBag());

            _elements.Add(X500NameOneAndOnly.stringIdentifer, new X500NameOneAndOnly());
            _elements.Add(X500NameBagSize.stringIdentifer, new X500NameBagSize());
            _elements.Add(X500NameIsIn.stringIdentifer, new X500NameIsIn());
            _elements.Add(X500NameBag.stringIdentifer, new X500NameBag());

            _elements.Add(Rfc822NameOneAndOnly.stringIdentifer, new Rfc822NameOneAndOnly());
            _elements.Add(Rfc822NameBagSize.stringIdentifer, new Rfc822NameBagSize());
            _elements.Add(Rfc822NameIsIn.stringIdentifer, new Rfc822NameIsIn());
            _elements.Add(Rfc822NameBag.stringIdentifer, new Rfc822NameBag());

            _elements.Add(IpAddressOneAndOnly.stringIdentifer, new IpAddressOneAndOnly());
            _elements.Add(IpAddressBagSize.stringIdentifer, new IpAddressBagSize());
            _elements.Add(IpAddressIsIn.stringIdentifer, new IpAddressIsIn());
            _elements.Add(IpAddressBag.stringIdentifer, new IpAddressBag());

            _elements.Add(DnsNameOneAndOnly.stringIdentifer, new DnsNameOneAndOnly());
            _elements.Add(DnsNameBagSize.stringIdentifer, new DnsNameBagSize());
            _elements.Add(DnsNameIsIn.stringIdentifer, new DnsNameIsIn());
            _elements.Add(DnsNameBag.stringIdentifer, new DnsNameBag());

            _elements.Add(StringConcatenate.stringIdentifer, new StringConcatenate());
            _elements.Add(BooleanFromString.stringIdentifer, new BooleanFromString());
            _elements.Add(StringFromBoolean.stringIdentifer, new StringFromBoolean());

            _elements.Add(IntegerFromString.stringIdentifer, new IntegerFromString());
            _elements.Add(StringFromInteger.stringIdentifer, new StringFromInteger());

            _elements.Add(DoubleFromString.stringIdentifer, new DoubleFromString());
            _elements.Add(StringFromDouble.stringIdentifer, new StringFromDouble());

            _elements.Add(TimeFromString.stringIdentifer, new TimeFromString());
            _elements.Add(StringFromTime.stringIdentifer, new StringFromTime());

            _elements.Add(DateTimeFromString.stringIdentifer, new DateTimeFromString());
            _elements.Add(StringFromDateTime.stringIdentifer, new StringFromDateTime());

            _elements.Add(AnyURIFromString.stringIdentifer, new AnyURIFromString());
            _elements.Add(StringFromAnyURI.stringIdentifer, new StringFromAnyURI());

            _elements.Add(DayTimeDurationFromString.stringIdentifer, new DayTimeDurationFromString());
            _elements.Add(StringFromDayTimeDuration.stringIdentifer, new StringFromDayTimeDuration());

            _elements.Add(YearMonthDurationFromString.stringIdentifer, new YearMonthDurationFromString());
            _elements.Add(StringFromYearMonthDuration.stringIdentifer, new StringFromYearMonthDuration());

            _elements.Add(X500NameFromString.stringIdentifer, new X500NameFromString());
            _elements.Add(StringFromX500Name.stringIdentifer, new StringFromX500Name());

            _elements.Add(Rfc822NameFromString.stringIdentifer, new Rfc822NameFromString());
            _elements.Add(StringFromRfc822Name.stringIdentifer, new StringFromRfc822Name());

            _elements.Add(DnsNameFromString.stringIdentifer, new DnsNameFromString());
            _elements.Add(StringFromDnsName.stringIdentifer, new StringFromDnsName());

            _elements.Add(StringEndsWith.stringIdentifer, new StringEndsWith());
            _elements.Add(AnyURIEndsWith.stringIdentifer, new AnyURIEndsWith());

            _elements.Add(StringStartsWith.stringIdentifer, new StringStartsWith());
            _elements.Add(AnyURIStartsWith.stringIdentifer, new AnyURIStartsWith());

            _elements.Add(StringContains.stringIdentifer, new StringContains());
            _elements.Add(AnyURIContains.stringIdentifer, new AnyURIContains());

            _elements.Add(StringSubstring.stringIdentifer, new StringSubstring());
            _elements.Add(AnyURISubstring.stringIdentifer, new AnyURISubstring());

            _elements.Add(AnyOf.stringIdentifer, new AnyOf());
            _elements.Add(AllOf.stringIdentifer, new AllOf());

            _elements.Add(AnyOfAny.stringIdentifer, new AnyOfAny());
            _elements.Add(AllOfAny.stringIdentifer, new AllOfAny());

            _elements.Add(AnyOfAll.stringIdentifer, new AnyOfAll());
            _elements.Add(AllOfAll.stringIdentifer, new AllOfAll());

            _elements.Add(Map.stringIdentifer, new Map());

            _elements.Add(X500NameMatch.stringIdentifer, new X500NameMatch());
            _elements.Add(Rfc822NameMatch.stringIdentifer, new Rfc822NameMatch());

            _elements.Add(StringRegexpMatch.stringIdentifer, new StringRegexpMatch());
            _elements.Add(AnyURIRegexpMatch.stringIdentifer, new AnyURIRegexpMatch());
            _elements.Add(IpAddressRegexpMatch.stringIdentifer, new IpAddressRegexpMatch());
            _elements.Add(DnsNameRegexpMatch.stringIdentifer, new DnsNameRegexpMatch());
            _elements.Add(Rfc822NameRegexpMatch.stringIdentifer, new Rfc822NameRegexpMatch());
            _elements.Add(X500NameRegexpMatch.stringIdentifer, new X500NameRegexpMatch());

            _elements.Add(XPathNodeCount.stringIdentifer, new XPathNodeCount());
            _elements.Add(XPathNodeEqual.stringIdentifer, new XPathNodeEqual());
            _elements.Add(XPathNodeMatch.stringIdentifer, new XPathNodeMatch());


            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:string-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:string-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:string-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:string-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:string-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:boolean-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:boolean-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:boolean-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:boolean-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:boolean-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:integer-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:integer-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:integer-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:integer-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:integer-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:double-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:double-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:double-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:double-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:double-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:time-intersection", new Intersection());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:time-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:time-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:time-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:time-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:date-intersection", new Intersection());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:date-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:date-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:date-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:date-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dateTime-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:dateTime-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dateTime-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dateTime-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dateTime-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:anyURI-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:anyURI-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:anyURI-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:anyURI-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:anyURI-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:hexBinary-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:hexBinary-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:hexBinary-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:hexBinary-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:hexBinary-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:base64Binary-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:base64Binary-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:base64Binary-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:base64Binary-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:base64Binary-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:dayTimeDuration-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-at-least-one-member-of",
                new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:yearMonthDuration-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:x500Name-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:x500Name-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:x500Name-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:x500Name-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:x500Name-set-equals", new TypeSetEquals());

            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:rfc822Name-intersection", new Intersection());
            _elements.Add(
                "urn:oasis:names:tc:xacml:1.0:function:rfc822Name-at-least-one-member-of", new AtLeastOneMemberOf());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:rfc822Name-union", new TypeUnion());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:rfc822Name-subset", new TypeSubset());
            _elements.Add("urn:oasis:names:tc:xacml:1.0:function:rfc822Name-set-equals", new TypeSetEquals());

            _elements.Add(AccessPermitted.stringIdentifer, new AccessPermitted());
        }

        public static Function GetInstance(string name)
        {
            Init();
            if (_elements.ContainsKey(name))
            {
                return _elements[name];
            }
            throw new IllegalExpressionEvaluationException("FunctionFactory");
        }

        public static DataTypeValue Evaluate(string name, DataTypeValue[] @params, EvaluationContext ctx)
        {
            Init();
            if (_elements.ContainsKey(name))
            {
                try
                {
                    Function func = _elements[name];
                    return func.Evaluate(@params, ctx);
                }
                catch (IllegalExpressionEvaluationException)
                {
                    throw new Indeterminate(Indeterminate.IndeterminateSyntaxError);
                }
            }
            throw new Indeterminate(Indeterminate.IndeterminateProcessingError);
        }

        public static void Init()
        {
            if (_factoryInstance == null)
            {
                _factoryInstance = new FunctionFactory();
                InitializeSupportedElements();
            }
        }
    }
}