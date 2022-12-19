using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using LoginRegister.Models;

namespace LoginRegister.Controllers
{
    public class AdminController : Controller
    {
        private DB_Entities db = new DB_Entities();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }




        // GET: Admin/Edit/5
        [HttpGet]
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

           
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([Bind(Include = "UserId,FirstName,MiddleName,UserStatus,LastName,FathersName,MobileNumber,Email,AadharNumber,DateofBirth,Address,Occupation,AnnualIncome,Password")] User _user)
        {
            MailMessage mm = new MailMessage("casestudyonlinebanking@gmail.com", _user.Email);



            mm.Subject = "Welcome to Online Banking";
            mm.Body = "Hello" + " " + _user.FirstName + " " + "Thank you for Registeration In OnlineBanking Application." + "This is your Username:" + _user.Email.ToString() + "   and this is your password: " + _user.Password.ToString() + "" + "You can now Login and get the benefit of Online Banking application";
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;



            NetworkCredential nc = new NetworkCredential("casestudyonlinebanking", "ndneepwnmskhnawt");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;



            smtp.Send(mm);



            ViewBag.message = "Thank you for Connecting with us!Your password has been sent to your regsitered mail id  ";


            return View(_user);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
