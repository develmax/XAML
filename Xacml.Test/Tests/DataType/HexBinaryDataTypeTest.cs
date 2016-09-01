namespace Xacml.Test.Tests.DataType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Xacml.Elements.DataType;
    using Xacml.Exceptions;

    [TestClass]
    public class HexBinaryDataTypeTest
    {
        [TestMethod]
        public virtual void TestGetInstance_String_IllegalLength()
        {
            try
            {
                HexBinaryDataType.GetInstance("bcf");
                Assert.Fail();
            }
            catch (Indeterminate e)
            {
                Assert.AreEqual(e.Message, Indeterminate.IndeterminateSyntaxError);
            }
        }

        [TestMethod]
        public virtual void TestGetInstance_String_IllegalCharacter()
        {
            try
            {
                HexBinaryDataType.GetInstance("rbcf");
                Assert.Fail();
            }
            catch (Indeterminate e)
            {
                Assert.AreEqual(e.Message, Indeterminate.IndeterminateSyntaxError);
            }
        }

        [TestMethod]
        public virtual void TestGetInstance_String_Null()
        {
            try
            {
                HexBinaryDataType.GetInstance("");
                Assert.Fail();
            }
            catch (Indeterminate e)
            {
                Assert.AreEqual(e.Message, Indeterminate.IndeterminateSyntaxError);
            }

            try
            {
                HexBinaryDataType.GetInstance("   \n");
                Assert.Fail();
            }
            catch (Indeterminate e)
            {
                Assert.AreEqual(e.Message, Indeterminate.IndeterminateSyntaxError);
            }
        }

        [TestMethod]
        public virtual void TestEquals()
        {
            Assert.AreEqual(HexBinaryDataType.GetInstance("0bcf"), HexBinaryDataType.GetInstance("0bcf"));
        }

        [TestMethod]
        public virtual void TestGetValue()
        {
            Assert.AreEqual("0bcf", HexBinaryDataType.GetInstance("0bcf").Value);
        }
    }
}