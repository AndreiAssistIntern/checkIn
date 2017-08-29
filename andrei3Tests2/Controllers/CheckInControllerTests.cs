using Xunit;
using andrei3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using andrei3.Models;
namespace andrei3.Controllers.Tests
{
    public class CheckInControllerTests
    {
        [Fact()]
        public void ShowTest()
        {
            CheckInController controler = new CheckInController();
            //  var result =  controler.Show();

            var results = controler.Show() as ViewResult;

            Assert.Equal(4, Add(2, 2));
            var model = results.Model as CheckIn;
            CheckInController check = new CheckInController();
            var a = check.Show();
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}