using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dogily_v2.Models;

namespace Dogily_v2.Controllers
{
    public class ProductDetailsController : Controller
    {
        private DogilyEntities6 db = new DogilyEntities6();

        // GET: ProductDetails
        public async Task<ActionResult> Index()
        {
            var productDetails = db.ProductDetails.Include(p => p.Brand).Include(p => p.Product).Include(p => p.Weight1);
            return View(await productDetails.ToListAsync());
        }

        // GET: ProductDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = await db.ProductDetails.FindAsync(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // GET: ProductDetails/Create
        public ActionResult Create()
        {
            ViewBag.BrandName = new SelectList(db.Brands, "BrandName", "About");
            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title");
            ViewBag.Weight = new SelectList(db.Weights, "Weight1", "Weight1");
            return View();
        }

        // POST: ProductDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProDeID,RemainAmount,Status,Description,Ingredients,IDPro,BrandName,Weight")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                db.ProductDetails.Add(productDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BrandName = new SelectList(db.Brands, "BrandName", "About", productDetail.BrandName);
            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title", productDetail.IDPro);
            ViewBag.Weight = new SelectList(db.Weights, "Weight1", "Weight1", productDetail.Weight);
            return View(productDetail);
        }

        // GET: ProductDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = await db.ProductDetails.FindAsync(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandName = new SelectList(db.Brands, "BrandName", "About", productDetail.BrandName);
            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title", productDetail.IDPro);
            ViewBag.Weight = new SelectList(db.Weights, "Weight1", "Weight1", productDetail.Weight);
            return View(productDetail);
        }

        // POST: ProductDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProDeID,RemainAmount,Status,Description,Ingredients,IDPro,BrandName,Weight")] ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandName = new SelectList(db.Brands, "BrandName", "About", productDetail.BrandName);
            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title", productDetail.IDPro);
            ViewBag.Weight = new SelectList(db.Weights, "Weight1", "Weight1", productDetail.Weight);
            return View(productDetail);
        }

        // GET: ProductDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = await db.ProductDetails.FindAsync(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
        }

        // POST: ProductDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductDetail productDetail = await db.ProductDetails.FindAsync(id);
            db.ProductDetails.Remove(productDetail);
            await db.SaveChangesAsync();
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
