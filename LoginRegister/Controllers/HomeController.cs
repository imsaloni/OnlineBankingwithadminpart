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
using System.Data.Entity.Validation;

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
            var UserId = (int)Session["UserId"];
            var Users = _db.Users.Where(t => t.UserId == UserId).FirstOrDefault();

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
            var AccountDetails = _db.AccountDetail.Where(t => t.UserId == UserId).FirstOrDefault();
            return View(AccountDetails);

        }
        //Transaction

        public ActionResult Transaction()
        {
            var UserId = (int)Session["UserId"];
            var AccountDetails = _db.AccountDetail.Where(t => t.UserId == UserId).FirstOrDefault();
           

            return View();


        }

        [HttpPost]

        public ActionResult Transaction(Transaction _transfer)
        {
            // var check = _db.Transaction.FirstOrDefault(s => s.TransationAmount == _transfer.AccBalance);

            var check = _db.AccountDetail.Where(Accbal => Accbal.AccountNumber == _transfer.AccountNumber).FirstOrDefault();
            var check1 = _db.AccountDetail.Where(Accbal => Accbal.AccountNumber == _transfer.payeeAccountNo).FirstOrDefault();
            if (_transfer.TransationAmount <= 0)
            {
                // throw new ApplicationException("Transfer Balance must be positive");
                ViewBag.error = "Balance is insuffient to make transaction";

            }
            else if (_transfer.TransationAmount == 0)
            {
                //throw new ApplicationException("Invalid Transfer Amount");
                ViewBag.error = "Insufficient balance";
            }
            //


            var TempData = check.Balance - _transfer.TransationAmount;
            check.Balance = TempData;
            var Transmoney = check1.Balance + _transfer.TransationAmount;
            check1.Balance = Transmoney;

            _db.Transaction.Add(_transfer);
            try
            {

                _db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property:" + validationError.PropertyName + "Error:" + validationError.ErrorMessage);
                    }
                    return RedirectToAction("Dashboard");
                }
            }
            return View();

        }




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
        public ActionResult Contact()
        {
            return View();
        }
        

        public ActionResult UserProfile(User us)



        {
            var UserId = (int)Session["UserId"];
            var Users = _db.Users.Where(t => t.UserId == UserId).FirstOrDefault();
            return View(Users);
        }
        public ActionResult AccountStatement()
        {
            var UserId = (int)Session["UserId"];
            var Transaction = _db.Transaction.Where(t => t.UserId == UserId).FirstOrDefault();
            return View(Transaction);

        }



    }



}

    

          