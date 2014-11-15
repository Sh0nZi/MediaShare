namespace MediaShare.Web.Tests.Common
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MediaShare.Common;

    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void GetUserName_WhenEmailIsValid_ShouldReturnUserName()
        {
            var validEmail = "pesho@pesho.com";

            var expected = "pesho";
            var actual = validEmail.ExtractUsernameFromMail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUserName_WhenEmailIsNotValid_ShouldThrowArgumentExcepton()
        {
            var validEmail = "peshoadsasdasdasd";

            var expected = "pesho";
            var actual = validEmail.ExtractUsernameFromMail();

            Assert.AreEqual(expected, actual);
        }
    }
}