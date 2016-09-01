namespace Xacml.Test.Tests.DataType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Xacml.Elements.DataType;

    [TestClass]
    public class IpAddressDataTypeTest
    {
        [TestMethod]
        public virtual void TestEncode()
        {
            string expResult = "10.110.10.10/255.255:10";
            var instance = new IpAddressDataType(expResult);
            string result = instance.Encode();
            Assert.AreEqual(expResult, result);
        }

        [TestMethod]
        public virtual void TestGetInstance_String()
        {
            string value = "10.10.10.10/255.255:10";
            DataTypeValue expResult = new IpAddressDataType("10.10.10.10/255.255:10");
            DataTypeValue result = IpAddressDataType.GetInstance(value);
            Assert.AreEqual(expResult, result);
        }

        [TestMethod]
        public virtual void TestEquals()
        {
            object o = new IpAddressDataType("10.10.10.10/255.255:10");
            var instance = (IpAddressDataType)IpAddressDataType.GetInstance("10.10.10.10/255.255:10");
            bool expResult = true;
            bool result = instance.Equals(o);
            Assert.AreEqual(expResult, result);
        }
    }
}