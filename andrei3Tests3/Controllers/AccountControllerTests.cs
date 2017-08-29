using Xunit;
using andrei3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using andrei3.Models;

namespace andrei3.Controllers.Tests
{
    public class AccountControllerTests
    {
        [Fact()]
        public void AccountControllerTest()
        {
            AccountController account = new AccountController();
            LoginViewModel dummy = new LoginViewModel();
            dummy.Email = "andreisileonte@gmail.com";
            dummy.Password = "WacaWaca22*";
            dummy.RememberMe = true;
            account.Login(dummy);

            
        }

        [Fact()]
        public void AccountControllerTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void LoginTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void LoginTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void VerifyCodeTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void VerifyCodeTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void RegisterTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void RegisterTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ConfirmEmailTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ForgotPasswordTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ForgotPasswordTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ForgotPasswordConfirmationTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ResetPasswordTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ResetPasswordTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ResetPasswordConfirmationTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ExternalLoginTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SendCodeTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SendCodeTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ExternalLoginCallbackTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ExternalLoginConfirmationTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void LogOffTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ExternalLoginFailureTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}