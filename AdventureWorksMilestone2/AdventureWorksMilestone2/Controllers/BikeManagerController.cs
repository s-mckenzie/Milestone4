using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorksMilestone2.Models;
using System.IO;

namespace AdventureWorksMilestone2.Controllers
{
    public class BikeManagerController : Controller
    {
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        // GET: BikeManager
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            var bikes = from x in db.Products where x.ProductCategoryID >= 5 && x.ProductCategoryID <= 7 select x;
            return View(bikes.ToList());
        }

        // GET: BikeManager/Details/5
        [Authorize(Roles = "Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // Used to search for unique product names
        [Authorize(Roles = "Manager")]
        public JsonResult CheckName(string name)
        {
            var names = from x in db.Products where x.Name == name select x;
            return ValidateItems(names, name);
        }

        // Used to search for unique product numbers
        [Authorize(Roles = "Manager")]
        public JsonResult CheckNumber(string productnumber)
        {
            var nums = from x in db.Products where x.ProductNumber == productnumber select x;
            return ValidateItems(nums, productnumber);
        }

        [Authorize(Roles = "Manager")]
        private JsonResult ValidateItems(IQueryable<Product> items, string item)
        {
            // Unique product name/number
            if (items.Count() == 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(item + " already exists.", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Manager")]
        public ActionResult GetBikeModel(int? id)
        {
            var model = (from x in db.vProductAndDescription2 where x.ProductCategoryID == id select new SelectListItem { Value = x.ProductModelID.ToString(), Text = x.ProductModel }).Distinct();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: BikeManager/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            var category = (from x in db.ProductCategories where x.ParentProductCategoryID == 1 select new SelectListItem { Value = x.ProductCategoryID.ToString(), Text = x.Name }).Distinct();
            ViewBag.ProductCategory = category;

            return View();
        }

        // POST: BikeManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                // Seems to be the only functioning way I could find
                var image = db.Products.FirstOrDefault(i => i.ThumbnailPhotoFileName == "no_image_available_small.gif");

                product.ModifiedDate = System.DateTime.Now;
                product.rowguid = Guid.NewGuid();
                product.ThumbNailPhoto = image.ThumbNailPhoto;
                product.ThumbnailPhotoFileName = "no_image_available_small.gif";

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var category = (from x in db.ProductCategories where x.ParentProductCategoryID == 1 select new SelectListItem { Value = x.ProductCategoryID.ToString(), Text = x.Name }).Distinct();
            ViewBag.ProductCategory = category;
            return View(product);
        }

        // GET: BikeManager/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var category = (from x in db.ProductCategories where x.ParentProductCategoryID == 1 select new SelectListItem { Value = x.ProductCategoryID.ToString(), Text = x.Name }).Distinct();
            ViewBag.ProductCategory = category;
            return View(product);
        }

        // POST: BikeManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product, int j)
        {
            if (ModelState.IsValid)
            {          
                product.ModifiedDate = System.DateTime.Now;
                product.rowguid = Guid.NewGuid();

                //Request["fileUpload"];

                // No new image was chosen
                if (product.ThumbNailPhoto == null)
                {
                    // Since the photos always turn null on edit, we need to fetch the product's details 
                    var item = db.Products.SingleOrDefault(i => i.ProductID == product.ProductID);
                    
                    // In case thumbnail photo was already null
                    if(item.ThumbNailPhoto == null)
                    {
                        // Get the byte array for no image
                        var image = db.Products.FirstOrDefault(i => i.ThumbnailPhotoFileName == "no_image_available_small.gif");
                        product.ThumbNailPhoto = image.ThumbNailPhoto;
                        product.ThumbnailPhotoFileName = "no_image_available_small.gif";
                    }
                    else
                    {
                        product.ThumbNailPhoto = item.ThumbNailPhoto;
                        product.ThumbnailPhotoFileName = item.ThumbnailPhotoFileName;
                    }   
                                  
                    // We have to set the new values this way instead or else Visual Studios will get confused with "item" and "product" sharing the same ID
                    // It's weird but this works
                    db.Entry(item).CurrentValues.SetValues(product);    
                }
                else
                {
                    db.Entry(product).State = EntityState.Modified;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var category = (from x in db.ProductCategories where x.ParentProductCategoryID == 1 select new SelectListItem { Value = x.ProductCategoryID.ToString(), Text = x.Name }).Distinct();
            ViewBag.ProductCategory = category;
            return View(product);
        }

        // GET: BikeManager/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: BikeManager/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
