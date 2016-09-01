namespace Xacml.Test.Tests.DataType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Xacml.Elements.DataType;
    using Xacml.Exceptions;

    [TestClass]
    public class PortRangeTest
    {
        [TestMethod]
        public virtual void TestEncode()
        {
            #region check on not correct range

            try
            {
                var pr1 = new PortRange("2a");
                Assert.Fail();
            }
            catch (Indeterminate e)
            {
                Assert.AreEqual(e.Message, Indeterminate.IndeterminateSyntaxError);
            }

            var pr2 = new PortRange("-2");
            Assert.AreEqual(pr2.PortStart, 1);

            try
            {
                var pr3 = new PortRange("2-a");
                Assert.Fail();
            }
            catch (Indeterminate e)
            {
                Assert.AreEqual(e.Message, Indeterminate.IndeterminateSyntaxError);
            }

            #endregion

            string expResult = "13-216";
            var instance = new PortRange(expResult);
            string result = instance.Encode();
            Assert.AreEqual(expResult, result);
        }

        [TestMethod]
        public virtual void TestEquals()
        {
            string expResult = "1-216";
            var instance = new PortRange(expResult);
            instance.Equals(new PortRange("-216"));
        }

        [TestMethod]
        public virtual void TestGetPortStart()
        {
            string expResult = "1-216";
            var instance = new PortRange(expResult);
            Assert.AreEqual(1, instance.PortStart);
        }

        [TestMethod]
        public virtual void TestGetPortEnd()
        {
            string expResult = "1-216";
            var instance = new PortRange(expResult);
            Assert.AreEqual(216, instance.PortEnd);
        }
    }
}