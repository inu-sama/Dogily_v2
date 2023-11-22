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
    public class ProImgsController : Controller
    {
        private DogilyEntities6 db = new DogilyEntities6();

        // GET: ProImgs
        public async Task<ActionResult> Index()
        {
            var proImgs = db.ProImgs.Include(p => p.Product);
            return View(await proImgs.ToListAsync());
        }

        // GET: ProImgs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProImg proImg = await db.ProImgs.FindAsync(id);
            if (proImg == null)
            {
                return HttpNotFound();
            }
            return View(proImg);
        }

        // GET: ProImgs/Create
        public ActionResult Create()
        {
            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title");
            return View();
        }

        // POST: ProImgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ImgID,ImgURL,MainImg,IDPro")] ProImg proImg)
        {
            if (ModelState.IsValid)
            {
                db.ProImgs.Add(proImg);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title", proImg.IDPro);
            return View(proImg);
        }

        // GET: ProImgs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProImg proImg = await db.ProImgs.FindAsync(id);
            if (proImg == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title", proImg.IDPro);
            return View(proImg);
        }

        // POST: ProImgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ImgID,ImgURL,MainImg,IDPro")] ProImg proImg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proImg).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDPro = new SelectList(db.Products, "IDPro", "Title", proImg.IDPro);
            return View(proImg);
        }

        // GET: ProImgs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProImg proImg = await db.ProImgs.FindAsync(id);
            if (proImg == null)
            {
                return HttpNotFound();
            }
            return View(proImg);
        }

        // POST: ProImgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProImg proImg = await db.ProImgs.FindAsync(id);
            db.ProImgs.Remove(proImg);
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
