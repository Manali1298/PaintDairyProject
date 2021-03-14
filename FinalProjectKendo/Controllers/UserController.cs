using FinalProjectKendo.DatabaseRepo;
using FinalProjectKendo.DataContext;
using FinalProjectKendo.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinalProjectKendo.Controllers
{
    public class UserController : Controller
    {
       
        UserDataRepo repo;
        ApplicationDbContext db = new ApplicationDbContext();

        public UserController()
        {
            repo = new UserDataRepo();
        }
        // GET: User
        public ActionResult Index()
        {
            

            return View();
        }

        //code for register
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterPanel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterPanel(UserData model)
        {
            int i = repo.RegisterUser(model);
            if (i >= 1)
            {
                int id = repo.GetUserId(model.u_email);
                //cartRepo.registerCartUser(id);
                return RedirectToAction("LoginPanel");
            }
            else
            {
                return View();
            }
        }
        //code for Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginPanel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPanel(string u_email, string u_password)
        {
            bool check = repo.signIn(u_email, u_password);
            if (check == true)
            {
                FormsAuthentication.SetAuthCookie(u_email, false);
                if (u_email != "manaligujju@gmail.com")
                {
                    ViewBag.message1 = "SucessFully Login";
                    return RedirectToAction("AlbumUserSideList", "AlbumStore");
                }
                else
                {
                    ViewBag.message1 = "SucessFully Login";
                    return RedirectToAction("AlbumAdminSideList", "AlbumStore");
                }
               
            }
            else
            {
                ViewBag.message2 = "Invalid Credential";
                return View();
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult SignOut(UserData model)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginPanel", "User");

        }
        [HttpGet]
        public ActionResult UserList()
        {
            return View();
        }

        //Json Methods
        public JsonResult GetUserData()
        {
            var users = repo.GetAllUser();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetSpecificUser(int id)
        {
            var user = repo.GetUserDetails(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}