using System;
using System.Linq;
using System.Web.Mvc;
using AB_CargoServices.Models;

namespace AB_CargoServices.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CargoController : Controller
    {
        /*
            public ActionResult Index()
            {
                return RedirectToAction("CargoSummary");
            }

            public ActionResult CargoSummary()
            {
                return View();
            }

            public ActionResult CreateCargo()
            {
                CargoDbHandler cdb = new CargoDbHandler();
                // populate cities for the dropdown list
                ViewBag.Cities = cdb.GetCities().OrderBy(c => c.CityName).ToList().Select(cc => new SelectListItem { Value = cc.CityName.ToString(), Text = cc.CityName }).ToList();

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult CreateSender(long tc, string fn, string ln, string address, string city, string phone)
            {
                if(ModelState.IsValid)
                {
                    Sender sender = new Sender(fn, ln, address, city, phone, tc);
                    ViewBag.Sender = sender;
                    CustomerDBHandler cdb = new CustomerDBHandler();
                    ViewBag.Message = cdb.CreateSender(sender);

                    //populate cities for the dropdownlist
                    CargoDbHandler cargoCities = new CargoDbHandler();
                    ViewBag.Roles = cargoCities.GetCities();

                    if (ViewBag.Message == "success")
                    {
                        return View("CargoSummary");
                    }
                    else
                    {
                        return View("CreateCargo");
                    }
                }
                return View("CreateCargo");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult CreateReceiver(string fn, string ln, string address, string city, string phone)
            {
                if (ModelState.IsValid)
                {
                    Receiver receiver = new Receiver(fn, ln, address, city, phone);
                    ViewBag.Receiver = receiver;
                    CustomerDBHandler cdb = new CustomerDBHandler();
                    ViewBag.Message = cdb.CreateReceiver(receiver);

                    if (ViewBag.Message == "success")
                    {
                        return View("CargoSummary");
                    }
                    else
                    {
                        return View("CreateCargo");
                    }
                }

                return View("CreateCargo");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult CreateCargo(float weight, float volume, long senderId, string description)
            {
                if(ModelState.IsValid)
                {
                    CargoViewModel cargo = new CargoViewModel(weight, volume, senderId, description);
                    ViewBag.Cargo = cargo;
                    CargoDbHandler cdb = new CargoDbHandler();
                    ViewBag.Message = cdb.AddNewCargo(cargo);

                    if (ViewBag.Message == "success")
                    {
                        return View("CargoSummary");
                    }
                    else
                    {
                        return View("CreateCargo");
                    }
                }

                return View();
            }


            public long ProcessTrackingId(string trackingId)
            {
                return Convert.ToInt64(trackingId);
            }
            public ActionResult TrackingInfo()
            {
                return View();
            }
            */

        public ActionResult GetTrackingInfo(string TrackingNo)
        {
            decimal TrackingID;
            if(string.IsNullOrEmpty(TrackingNo))
            {
                ViewBag.TrackerMsg = "";
                return View();
            }
            else if(!(Decimal.TryParse(TrackingNo, out TrackingID)))
            {
                ViewBag.TrackerMsg = "Please enter an 8 digit number.";
                return View();
            }

            TrackerDb tdb = new TrackerDb();
            TrackingDetailsViewModel TrackInfo = new TrackingDetailsViewModel();
            TrackInfo = tdb.TrackingInfo(TrackingID);

            if (TrackInfo != null)
            {
                ModelState.Clear();
                ViewBag.TrackerMsg = "Correct Tracking ID";
                return View(TrackInfo);
            }

            ViewBag.TrackerMsg = "Tracking ID does not exist in our database!";
            return View();
        }
    }
}