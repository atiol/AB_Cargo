using AB_CargoServices.Models;
using System.Linq;
using System.Web.Mvc;

namespace AB_CargoServices.Controllers
{
    public class SearchController : Controller
    {

        AB_CargoEntities db = new AB_CargoEntities();
        // GET: Search
        public ActionResult Query(string q)
        {
            decimal d;
            if(decimal.TryParse(q, out d))
            {
                System.Console.WriteLine("couldn't parse string");
            }
            // Return customers/senders
            return View(db.SENDERs.Where(x => x.LAST_NAME.Contains(q.ToUpper()) || x.FIRST_NAME.Contains(q.ToUpper()) || x.TC.Equals(d) || q == null).ToList());
        }
    }
}