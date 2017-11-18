using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AB_CargoServices.Models;

namespace AB_CargoServices.Controllers
{
    public class HomeController : Controller
    {
        private AB_CargoEntities db = new AB_CargoEntities();

        private TrackerDb tdb = new TrackerDb();
        private TrackingDetailsViewModel _trackInfo;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            TrackingDetailsViewModel result = GetTrackingInfo(collection["trackingNo"]);
            if (result == null)
            {
                ViewBag.TrackerMsg = "Incorrect Tracking ID";
                return View();
            }
                
            ModelState.Clear();
            ViewBag.TrackerMsg = "Correct Tracking ID";
            return View(result);
        }

        public TrackingDetailsViewModel GetTrackingInfo(string trackingNo)
        {
            decimal trackingId;
            if (string.IsNullOrEmpty(trackingNo))
            {
                return null;
            }
            if (!(decimal.TryParse(trackingNo, out trackingId)))
            {
                ViewBag.TrackerMsg = "Please enter an 8 digit number.";
                return null;
            }

            _trackInfo = new TrackingDetailsViewModel();
            _trackInfo = tdb.TrackingInfo(trackingId);
            
            return _trackInfo;
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessUserMessages(FormCollection collection)
        {
            DateTime thisday = Convert.ToDateTime(DateTime.Today.ToString("d"));

            try
            {
                db.USER_MESSAGES.Add(new USER_MESSAGES()
                    {
                        NAME = collection["userName"],
                        EMAIL = collection["userEmail"],
                        MESSAGE = collection["userMessage"],
                        SENT = thisday
                    }
                    );
                db.SaveChanges();
                ViewBag.status = "Message sent. We'll get back to you as soon as possible.";
                //ModelState.Clear();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.status = e.Message;
                return View("Index");
            }
        }
        

        // GET: User Profile photo
        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/images/avatar.png");

                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    var imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }
                // to get the user details to load user Image    
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.FirstOrDefault(x => x.Id == userId);

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/images/avatar.png");

                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                var imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }
    }
}