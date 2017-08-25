using andrei3.Models;
using andrei3.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace andrei3.Controllers
{
    public class CustomLoginController : ApiController
    {
        private Secure secure;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();    
        public ApplicationUserManager UserManager;


        public CustomLoginController()
        {
            secure = new Secure();
            _context.Configuration.ProxyCreationEnabled = false;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Login/Create")]
        public async Task<String> PostUser([FromBody] RegisterViewModel user)
        {
            AccountController accountController = new AccountController();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            ApplicationUserManager _userManager = new ApplicationUserManager(store);

            var manger = _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user1 = new ApplicationUser() { Email = user.Email, UserName = user.Email };
            var result = await manger.CreateAsync(user1, user.Password);
            if (result.Succeeded)
            {

                ApplicationUser user2 = new ApplicationUser();

                user1 = _context.Users.First(m => m.Email == user.Email);
                user1.IsLogin = true;

                _context.Entry(user1).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                string errors = "";
                foreach (var r in result.Errors.ToList())
                {
                    errors += r + "\n";
                }
                return errors;
            }


            return "Succesfull created the user";

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Login/Login")]
        public async Task<String> PostUser1([FromBody] LoginViewModel user)
        {

            bool check = await this.secure.LoginIn(user.Email, user.Password);
            return check ? "Login succesfull" : "Bad credentials";
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Login/Test")]
        public string testStaticKey()
        {
            return checkUser() ? "The user is logged in" : "The user is not logged in";
        }
        public bool checkUser()
        {
            string secure = "";
            secure = Request.Headers.GetValues("Authorization").FirstOrDefault();
            bool check = this.secure.checkUser(secure);
            return check;
        }
    
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Login/LoginDinamic")]
        public async Task<String> PostUser2([FromBody] LoginViewModel user)
        {

            bool check = await this.secure.LoginInDinamicKey(user.Email, user.Password);
            return check ? "Login succesfull" : "Bad credentials";
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Login/TestStaticKey")]
        public async Task<string> testDinamicKey()
        {
            bool check =await checkUserDinamic();
            return check ? "The user is logged in" : "The user is not logged in";
        }

       
        public async Task<bool> checkUserDinamic()
        {
            string authorization1 = "";
            string authorization2 = "";

            authorization1 = Request.Headers.GetValues("Key").FirstOrDefault();
            authorization2 = Request.Headers.GetValues("Authorization").FirstOrDefault();
            bool check = await this.secure.checkUserDinamicKey(authorization1, authorization2);

            return check;
        }

    }
}