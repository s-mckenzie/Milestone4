using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorksMilestone2.Models;

namespace AdventureWorksMilestone2.Controllers
{
    public class BikeController : Controller
    {
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        // GET: Bike
        public ActionResult Index()
        {
            var categ = from x in db.ProductCategories where x.ParentProductCategoryID == 1 select x;

            return View(categ);
        }

        public ActionResult getBikesByID(int id)
        {
            var bikes = from x in db.vProductAndDescription2 where x.Culture == "en" && x.ProductCategoryID == id select x;
            return View(bikes);
        }

        // GET: Bike/Mountain
        public ActionResult Mountain()
        {
            return getBikesByID(5);
        }

        // GET: Bike/Road
        public ActionResult Road()
        {
            return getBikesByID(6);
        }

        // GET: Bike/Touring
        public ActionResult Touring()
        {
            return getBikesByID(7);
        }


        // GET: Bike/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var details = from x in db.Products where x.ProductModelID == id select x;
            return View(details.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
