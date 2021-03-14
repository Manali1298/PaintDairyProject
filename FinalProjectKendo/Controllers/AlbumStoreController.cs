using FinalProjectKendo.DatabaseRepo;
using FinalProjectKendo.DataContext;
using FinalProjectKendo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectKendo.Controllers
{
    public class AlbumStoreController : Controller
    {
        AlbumDataRepo repo;
        UserDataRepo repou;
        CartRepo repoc;
        public AlbumStoreController()
        {
            repo = new AlbumDataRepo();
            repou = new UserDataRepo();
            repoc = new CartRepo();
        }
        ApplicationDbContext db = new ApplicationDbContext();
        
        //add album
        [HttpGet]
        public ActionResult UploadAlbum()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult UploadAlbum(HttpPostedFileBase al_img, AlbumData model)
        {

            if (ModelState.IsValid)
            {
                if (al_img.ContentLength > 0)
                {
                    string path = Server.MapPath("~/Uploadalbum");
                    string filename = Path.GetFileName(al_img.FileName);
                    string fullpath = Path.Combine(path, filename);
                    string imagepath = Path.Combine("/Uploadalbum/", filename);
                    try
                    {
                        al_img.SaveAs(fullpath);
                    }
                    catch (Exception ex)
                    {
                        return Content(ex.Message);
                    }
                    model.al_img = imagepath;
                    int i = repo.AddAlbum(model);
                    if (i >= 1)
                    {
                        return RedirectToAction("AlbumAdminSideList");
                    }
                    else
                    {
                        ViewBag.msg = "Failed";
                        return View();
                    }
                }
                else
                {
                    ViewBag.msg = "Failed";
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "Failed";
                return View();
            }
        }

        //Edit album
        [HttpGet]
        public JsonResult EditPhoto(int id)
        {
            TempData["a_id"] = id;
            AlbumData model = new AlbumData();
            //model = repo.AlbumDetailAdmin(id);
            model = repo.GetAlbumDetails(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditPhoto(HttpPostedFileBase a_id, AlbumData model)
        {
            model.a_id = Convert.ToInt32(TempData["a_id"]);
            int i = repo.editAlbum(model);
            if (i >= 1)
            {
                return RedirectToAction("AlbumAdminSideList");
            }
            else
            {
                ViewBag.msg = "Failed";
                return View();
            }
        }
        // delete album
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int i = repo.DeleteAlbum(id);
            if(i >= 1)
            {
                return RedirectToAction("AlbumAdminSideList");
            }
            return View();
        }
        public JsonResult ListAllPhoto()
        {
            var photo = repo.AllAlbum();
            var jsonResult = Json(photo, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public ActionResult SpecificAlbum()
        {
          
            return View();
        }
        [HttpGet]
        public ActionResult AlbumUserSideList()
        {
            int id = repou.GetUserId(User.Identity.Name);
            ViewBag.totalprise = repo.totalPrise(User.Identity.Name);
            return View();
        }
        [HttpGet]
        public ActionResult AlbumAdminSideList()
        {
            return View();
        }
       //public JsonResult EditPhoto(int id)
       // {
       //     TempData["a_id"] = id;
       //     AlbumData model = new AlbumData();
       //     model = repo.GetAlbumDetails(id);
       //     return Json(model, JsonRequestBehavior.AllowGet);
       // }

       
        public JsonResult GetSpecificAlbum(int id)
        {
            var album = repo.GetAlbumDetails(id);
           
           var jsonresult=Json(album, JsonRequestBehavior.AllowGet);
            return jsonresult;
        }
        public JsonResult PhotoDetail(int id)
        {
            var result = repo.GetAlbumDetails(id);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        //Json Methods
        public JsonResult GetUserData()
        {
            var users = repo.GetAllAlbum();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
       
        [Authorize]
        public ContentResult AddCart(int photo_id, int quantity)
        {
            int id = repou.GetUserId(User.Identity.Name);
            
                int i = repoc.addCart(photo_id, id, quantity);
                if (i >= 1)
                {
                        return Content("Succesfully Added");
                 
                }
                else
                {
                    return Content("Failed");
                }
            
        }
        [Authorize]
        public JsonResult ListCart()
        {
            int i = repou.GetUserId(User.Identity.Name);
           
                var data = repoc.ListCart(i);
           
            var jsonresult = Json(data, JsonRequestBehavior.AllowGet);
            return jsonresult;
        }
        
        public ActionResult ListcartData()
        {
            return View();
        }
        public JsonResult UpdatePrise(int photo_id, int cart_itemqua)
        {
            int i = 0;
            AlbumDatabase mod = new AlbumDatabase();
            mod = repoc.detail(photo_id, repou.GetUserId(User.Identity.Name));
            if (mod.cart_itemqua > cart_itemqua)
            {
                i = repoc.DecreesCartItem(repou.GetUserId(User.Identity.Name), photo_id);
            }
            else
            {
                i = repoc.updateprise(photo_id, repou.GetUserId(User.Identity.Name));
            }

            if (i >= 0)
            {
                repo.UpdatesellQuantity(cart_itemqua, photo_id);
                int total_prise = repoc.totalPrise(repou.GetUserId(User.Identity.Name), photo_id);
                var jsonResult = Json(total_prise, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            else
            {
                var message = "failed";
                var jsonResult = Json(message, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
        }
        public static int GenerateRandomInt(Random rnd)
        {
            return rnd.Next();
        }
        [Authorize]
        public ActionResult RemoveItem(int photo_id)
        {
            int id = repou.GetUserId(User.Identity.Name);
            
                int i = repoc.removeItem(id, photo_id);
                if (i >= 1)
                {
                     ViewBag.message3 = "Sucessfully Remove";
                    return RedirectToAction("ListcartData");
                }
                else
                {
                    return RedirectToAction("AlbumUserSideList");
                }
           
        }
        public JsonResult TotalPrise()
        {
            int total_prise = repo.totalPrise(User.Identity.Name);
            var jsonResult = Json(total_prise, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            ViewBag.totalprise = repo.totalPrise(User.Identity.Name);
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Checkout(CheckOutData model)
        {
            Random rnd = new Random();
            model.order_num = GenerateRandomInt(rnd);
            model.u_id = repou.GetUserId(User.Identity.Name);
            model.cart_id = repoc.getCartId(model.u_id);
            model.totalprice = repo.totalPrise(User.Identity.Name);
            int i = repoc.CheckOut(model);
            if (i >= 1)
            {
                //repoc.DeleteAllCartItem(model.u_id);
                return RedirectToAction("CheckoutDetail");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult CheckoutDetail()
        {
            int id = repou.GetUserId(User.Identity.Name);
           CheckOutData mod = new CheckOutData();
            mod = repoc.OrderDetails(id);
            ViewBag.order_number = mod.order_num;
            ViewBag.total = mod.totalprice;
            return View();
        }
      
        public JsonResult checkoutdata()
        {
          
            var jsonresult = Json(repo.checkoutbarchart(), JsonRequestBehavior.AllowGet);
            return jsonresult;
        }
        [HttpGet]
        public ActionResult Barchart()
        {
            return View();
        }

        public JsonResult sellData()
        {

            var jsonresult = Json(repo.sellalbumcount(), JsonRequestBehavior.AllowGet);
            ViewBag.data = jsonresult;
            return jsonresult;
        }
        [HttpGet]
        public ActionResult sellchart()
        {
            return View();
        }
    }
}