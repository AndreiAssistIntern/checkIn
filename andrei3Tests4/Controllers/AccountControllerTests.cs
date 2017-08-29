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
    public class AccountControllerTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            AccountController account = new AccountController();
            LoginViewModel dummy = new LoginViewModel();
            dummy.Email = "andreisileonte@gmail.com";
            dummy.Password = "WacaWaca22*";
            dummy.RememberMe = true;
            account.Login(dummy);
            Assert.Fail();
        }
    }
}