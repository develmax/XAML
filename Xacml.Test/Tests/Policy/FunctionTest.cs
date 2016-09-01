namespace Xacml.Test.Tests.Policy
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Xacml.Elements.Policy;
    using Xacml.Types.Streams;
    using Xacml.Utils;

    [TestClass]
    public class FunctionTest
    {
        [TestMethod]
        public virtual void TestGetInstance_String()
        {
            Function.GetInstance("<Function FunctionId=\" test \" />").Encode(
                new OutputStream(Console.Out), new Indentation());
        }
    }
}