using Xunit;
using andrei3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using andrei3.Models;
using System.Web.Script.Serialization;

namespace andrei3.Controllers.Tests
{
    public class CustomLoginControllerTests
    {
        [Fact()]
        public async Task CustomLoginControllerTest()
        {
            CustomLoginController custom = new CustomLoginController();
            RegisterViewModel dummy = new RegisterViewModel();
           
            dummy.Email = "andreisileonte@gmail.com";//change for testing
            dummy.Password = "WacaWaca22*";
            dummy.ConfirmPassword = "WacaWaca22*";
            string result="";
            try
            {
                result = await custom.PostUser(dummy);
            }
            catch(Exception e)
            {
                Assert.Contains("Succesfull created the user",result);
            }
                Assert.Contains("Succesfull created the user", result);
        }

        [Fact()]
        public void PostUserTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void PostUser1Test()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void testStaticKeyTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void checkUserTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void PostUser2Test()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void testDinamicKeyTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void checkUserDinamicTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}