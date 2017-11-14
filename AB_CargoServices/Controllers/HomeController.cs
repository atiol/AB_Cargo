﻿using AB_CargoServices.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AB_CargoServices.Controllers
{
    public class HomeController : Controller
    {
        private TrackerDb tdb = new TrackerDb();
        private TrackingDetailsViewModel _trackInfo;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string trackingNo)
        {
            TrackingDetailsViewModel result = GetTrackingInfo(trackingNo);
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
            if (!(Decimal.TryParse(trackingNo, out trackingId)))
            {
                ViewBag.TrackerMsg = "Please enter an 8 digit number.";
                return null;
            }

            _trackInfo = new TrackingDetailsViewModel();
            _trackInfo = tdb.TrackingInfo(trackingId);
            
            return _trackInfo == null ? null : _trackInfo;
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

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }
                // to get the user details to load user Image    
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/images/avatar.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }
    }
}