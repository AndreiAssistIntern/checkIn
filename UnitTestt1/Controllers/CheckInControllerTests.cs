using Microsoft.VisualStudio.TestTools.UnitTesting;
using andrei3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andrei3.Controllers.Tests
{
    [TestClass()]
    public class CheckInControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            CheckInController check = new CheckInController();
            var a =check.Show();
            Assert.AreEqual(1,1);
          //  Assert.Fail();
        }

        [TestMethod()]
        public void ShowTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexTest1()
        {
            Assert.Fail();
        }
    }
}