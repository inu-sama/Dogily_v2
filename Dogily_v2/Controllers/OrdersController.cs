using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dogily_v2.Models;

namespace Dogily_v2.Controllers
{
    public class OrdersController : Controller
    {
        private DogilyEntities6 db = new DogilyEntities6();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.City1).Include(o => o.District1).Include(o => o.Discount);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.City = new SelectList(db.Cities, "IDCity", "CityName");
            ViewBag.District = new SelectList(db.Districts, "IDDistrict", "DistrictName");
            ViewBag.DiscountCode = new SelectList(db.Discounts, "DiscountCode", "DiscountName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDOrder,TotalPrice,OrderDate,CusName,CusPhone,CusEmail,City,District,Adress,Note,DiscountCode")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.City = new SelectList(db.Cities, "IDCity", "CityName", order.City);
            ViewBag.District = new SelectList(db.Districts, "IDDistrict", "DistrictName", order.District);
            ViewBag.DiscountCode = new SelectList(db.Discounts, "DiscountCode", "DiscountName", order.DiscountCode);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.City = new SelectList(db.Cities, "IDCity", "CityName", order.City);
            ViewBag.District = new SelectList(db.Districts, "IDDistrict", "DistrictName", order.District);
            ViewBag.DiscountCode = new SelectList(db.Discounts, "DiscountCode", "DiscountName", order.DiscountCode);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDOrder,TotalPrice,OrderDate,CusName,CusPhone,CusEmail,City,District,Adress,Note,DiscountCode")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.City = new SelectList(db.Cities, "IDCity", "CityName", order.City);
            ViewBag.District = new SelectList(db.Districts, "IDDistrict", "DistrictName", order.District);
            ViewBag.DiscountCode = new SelectList(db.Discounts, "DiscountCode", "DiscountName", order.DiscountCode);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
