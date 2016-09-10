using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookWebApp.BitBook.Core.BLL;
using BitBookWebApp.Context;
using BitBookWebApp.Models;


namespace BitBookWebApp.Controllers
{
    public class UserRegistrationController : Controller
    {
        
        UserRegistrationManager aUserRegistrationManager = new UserRegistrationManager();
        
        // GET: UserRegistration


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(User aUser)
        {

            bool isExists = aUserRegistrationManager.IsEmailAleadyExist(aUser.Email);
            if (isExists)
            {
                ViewBag.StatusMessage = "Email Already Exists.";
                
            }
            else
            {
                bool isSaved = aUserRegistrationManager.SaveUserRegistraion(aUser);

                if (isSaved)
                {
                    ViewBag.StatusMessage = "Save is successed.";
                }
                else
                {
                    ViewBag.StatusMessage = "Saved Fail";
                }
            }

            
            return View();
        }


        public ActionResult IsEmailExist(string Email)
        {
            using (BitBookContext db=new BitBookContext())
            {
                try
                {
                    var email = db.Users.Single(m => m.Email == Email);
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {

                    return Json(true, JsonRequestBehavior.AllowGet);
                    
                }
            }
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogViewModel lg)
        {
            if (ModelState.IsValid)
            {
                using (BitBookContext aBitBookContext = new BitBookContext())
                {
                    var log = aBitBookContext.Users.Where(a => a.Email.Equals(lg.Email) && a.Password.Equals(lg.Password)).FirstOrDefault();
                    if (log!=null)
                    {
                        Session["email"] = log.Email;
                        return RedirectToAction("UserHome","UserRegistration");
                    }

                    else
                    {
                        Response.Write("<script> alert('Invalid Password')</script>");
                    }
                }
            }
            return View();
        }

        public ActionResult UserHome()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "UserRegistration");
        }
    }
}