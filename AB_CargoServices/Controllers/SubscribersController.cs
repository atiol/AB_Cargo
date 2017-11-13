using AB_CargoServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AB_CargoServices.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class SubscribersController : Controller
    {
        private AB_CargoEntities db = new AB_CargoEntities();
        // GET: Subscribers
        public ActionResult Index()
        {
            /*
            SubscribersDBHandler subscribers = new SubscribersDBHandler();
            ModelState.Clear();
            if (subscribers.GetSubscribers() != null)
            {
                ViewBag.Message = "Successfully retrieved subscribers!";
                return View(subscribers.GetSubscribers());
            }
            else
            {
                ViewBag.Message = "There are no subscribers available in the database.";
                return View();
            }
            */
            
            return View(db.SUBSCRIBERS.ToList());
        }

        // DELETE : Subscribers
        public ActionResult Delete(string email)
        {
            SubscribersDBHandler subscriber = new SubscribersDBHandler();
            if(subscriber.DeleteSubscriber(email))
            {
                ViewBag.Message = "Successfully deleted Subscriber";
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Message = "Error Deleting user";
                return View();
            }
        }

        // CREATE : Subscribers

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Subscribers subscriber)
        {
            if(ModelState.IsValid)
            {
                SubscribersDBHandler sdb = new SubscribersDBHandler();

                if (sdb.CreateSubscriber(subscriber))
                {
                    ViewBag.Message = "Successfully new subscriber added!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "ERROR: "+sdb.CreateSubscriber(subscriber);
                    return View();
                }
            }
            return View();
        }
    }
}