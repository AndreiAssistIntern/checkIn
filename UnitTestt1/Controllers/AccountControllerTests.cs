
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace UnitTestt1.Controllers
{
  public  class AccountControllerTests
    {
        [Fact]
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void test()
        {
            Assert.Equal(4,Add(2,2));
        }

        int Add(int x,int y)
        {
            return x + y;
        }
    }
}
