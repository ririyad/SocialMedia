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
            if (Session["email"] != null)
            {
                return View();
                
            }
            else
            {
                return RedirectToAction("Login", "UserRegistration");
                               
            }
 
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "UserRegistration");
        }


        public ActionResult AddBasicInfo(HttpPostedFileBase file)
        {

            string userEmail = "";
            userEmail=   Session["email"].ToString();
            if (Session["email"] != null)
            {
                BitBookContext db = new BitBookContext();


                var user=db.Users.Where(x => x.Email.Equals(userEmail)).FirstOrDefault();
                ////var userId = db.BasicInformaionts.FirstOrDefault(x => x.UserId.Equals(user.Id));


                if (db.BasicInformaionts.Any(x => x.UserId.Equals(user.Id)))
                {
                    //Update user Info.

                    return RedirectToAction("Display", "UserRegistration");


                }
                else
                {

                    if (file != null)
                    {
                        //ProfilePic
                        string ProfilePicName = System.IO.Path.GetFileName(file.FileName);
                        string physicalPathProfile = Server.MapPath("~/image/ProfilePic/" + ProfilePicName);

                        // save image in folder
                        file.SaveAs(physicalPathProfile);

                        //CoverPic
                        string CoverPicName = System.IO.Path.GetFileName(file.FileName);
                        string physicalPathCover = Server.MapPath("~/image/CoverPic/" + CoverPicName);

                        // save image in folder
                        file.SaveAs(physicalPathCover);


                        //save new record in database
                        BasicInfo newRecord = new BasicInfo();
                        newRecord.AreaOfInterest = Request.Form["AreaOfInterest"];
                        newRecord.Location = Request.Form["Location"];
                        newRecord.Experience = Request.Form["Experience"];
                        newRecord.Education = Request.Form["Education"];
                        newRecord.UserId = user.Id;
                        newRecord.ProfilePhotoUrl = ProfilePicName;


                        newRecord.CoverPhotoUrl = CoverPicName;
                        db.BasicInformaionts.Add(newRecord);
                        db.SaveChanges();
                        return RedirectToAction("Display", "UserRegistration");

                    }
                    else
                    {
                        return RedirectToAction("AddBasicInfo", "UserRegistration");
                        
                    }

                }



            }
            else
            {
                return RedirectToAction("Login", "UserRegistration");

            }

            
            
        }



        public ActionResult Display()
        {
            return View();
        }


    }
}