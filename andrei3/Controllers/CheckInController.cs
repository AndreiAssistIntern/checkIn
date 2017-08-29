using andrei3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace andrei3.Controllers
{
    [Authorize]
    public class CheckInController : Controller
    {
        // GET: CheckIn
        public ActionResult Index()
        {

            return View();
        }
    
        [AllowAnonymous]
        public ActionResult Show()
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<ViewModelCheckIn> checkIn = new List<ViewModelCheckIn>();
                var records = db.checkIn.ToList();
                foreach (var r in records)
                {
                    ViewModelCheckIn ch = new ViewModelCheckIn();
                    ch.hasChecked = r.hasChecked;
                    ch.checkUser = r.checkUser;
                    ch.userEmail = db.Users.FirstOrDefault(m => m.Id == r.UserId).Email;
                    checkIn.Add(ch);
                }
                return View(checkIn);
            }

        }
        [HttpPost]
        public ActionResult Index(CheckIn check)
        {
            check.checkUser = DateTime.Now.ToString("h:mm:ss tt");

            String userName = System.Web.HttpContext.Current.User.Identity.Name;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user1 = new ApplicationUser();

                user1 = db.Users.First(m => m.Email == userName);
                check.UserId = user1.Id;
                check.hasChecked = true;
                db.checkIn.Add(check);
                db.SaveChanges();
                db.Entry(user1).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Index();
        }
    }
}