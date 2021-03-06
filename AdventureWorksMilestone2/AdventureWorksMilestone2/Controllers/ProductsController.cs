﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorksMilestone2.Models;
using System.Diagnostics;

namespace AdventureWorksMilestone2.Controllers
{
    public class ProductsController : Controller
    {
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        // GET: Products
        public ActionResult Index()
        {
            ViewBag.Ratings = from x in db.Reviews select x;

            var cats = from x in db.ProductCategories where x.ParentProductCategoryID > 1 select x.Name;

            ViewBag.Categories = cats;
            var products = from x in db.Products where x.SellEndDate == null select x;
            return View(products);
        }
        [HttpPost]
        public ActionResult Index(String category)
        {
            ViewBag.searchParam = category;

            return Index();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Id = id;
            Product product = db.Products.Find(id);
            ViewBag.reviews = from x in db.Reviews where x.ProductID == id select x ;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}
