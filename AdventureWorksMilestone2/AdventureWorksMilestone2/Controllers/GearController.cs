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
    public class GearController : Controller
    {
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        enum ParentCategories { Components = 2, Clothing, Accessories };

        // GET: Gear
        public ActionResult Index()
        {
            // Select categories between components (2) and accessories (4)
            var categ = from x in db.ProductCategories where x.ProductCategoryID >= (int)ParentCategories.Components && x.ProductCategoryID <= (int)ParentCategories.Accessories select x;
            return View(categ);
        }

        public ActionResult getGearsByID(int id)
        {
            var gears = from x in db.ProductCategories where x.ParentProductCategoryID == id select x;

            return View(gears);
        }

        public ActionResult Components()
        {
            return getGearsByID((int)ParentCategories.Components);
        }

        public ActionResult Clothing()
        {
            return getGearsByID((int)ParentCategories.Clothing);
        }

        public ActionResult Accessories()
        {
            return getGearsByID((int)ParentCategories.Accessories);
        }

        // GET: Gear/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);*/

            var details = from x in db.Products where x.ProductCategoryID == id && x.SellEndDate == null select x;
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
