using Xunit;
using andrei3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using andrei3.Models;
using System.Web.Mvc;

namespace andrei3.Controllers.Tests
{
    public class AccountControllerTests
    {
        [Fact()]
        public void LoginTest()
        {
            AccountController account = new AccountController();
            LoginViewModel dummy = new LoginViewModel();
            dummy.Email = "andreisileonte@gmail.com";
            dummy.Password = "WacaWaca22*";
            dummy.RememberMe = true;
            var result = account.Login(dummy,"Succes");


            Assert.True(false, "This test needs an implementation");
        }
    }
}