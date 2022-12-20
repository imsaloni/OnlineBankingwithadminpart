using LoginRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Data.Entity;
using System.Net;
using System.Security.Principal;
using System.Net.Mail;

namespace LoginRegister.Controllers
{
    public class HomeController : Controller
    {
        private DB_Entities _db = new DB_Entities();

       

        // GET: Home
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Admin");
            }
        }

        //Dashboard

        public ActionResult Dashboard()
        {
            return View();
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }

            // email notfication
            MailMessage mm = new MailMessage("casestudyonlinebanking@gmail.com", _user.Email);



            mm.Subject = "Welcome to Online Banking";
            mm.Body = "Hello" + " " + _user.FirstName + " " + "Thank you for Registeration In OnlineBanking Application." + "This is your Username:" + _user.Email.ToString() + "   and this is your password: ";
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




            return RedirectToAction("Login");
        }
            


        

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["UserId"] = data.FirstOrDefault().UserId;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        //Account Details

        public ActionResult AccountDetails()
        {
            var UserId = (int)Session["UserId"];
            var Users = _db.Users.Where(t => t.UserId == UserId).FirstOrDefault();
            return View(Users);

        }
        //Transaction
        [HttpGet]
        public ActionResult Transaction()
        {
            return View();  
        }
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transactions()
        {
            var accountDetails = _db.AccountDetails.ToList();
           
            var user1 = _db.AccountDetails.ToList();
            AccountDetails user = _db.AccountDetails.Find();
            var user2 = _db.AccountDetails.FindAsync();
            user1.Balance -= Balance;
            user2.Credit += Balance;

            await db.SaveChangesAsync();
            return View();
        }*/


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(Password);
                var data = _db.Admins.Where(s => s.Email.Equals(Email) && s.Password.Equals(Password)).ToList();
                if (data.Count() > 0)
                {
                    Session["Email"] = data.FirstOrDefault().Email;


                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.msg = "Invalid Adminid or Password";
                }
            }
            return View();
        }
        

        public ActionResult UserProfile(User us)



        {
            var UserId = (int)Session["UserId"];
            var Users = _db.Users.Where(t => t.UserId == UserId).FirstOrDefault();
            return View(Users);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            var user = _db.Users.SingleOrDefault(e => e.UserId == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }



        [HttpPost]
        public ActionResult Edit(User use)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(User).State = EntityState.Modified;
                _db.Users.Attach(use);
                _db.SaveChanges();
                return RedirectToAction("Index", "UserProfile");
            }



            return View();
        }



    }



}

    

          