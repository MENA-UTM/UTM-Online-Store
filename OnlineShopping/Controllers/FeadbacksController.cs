using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class FeadbacksController : Controller
    {
        private OnlineShoppingEntities2 db = new OnlineShoppingEntities2();

        // GET: Feadbacks
        public ActionResult Index()
        {
            var feadbacks = db.Feadbacks.Include(f => f.Customer).Include(f => f.Product);
            return View(feadbacks.ToList());
        }

        // GET: Feadbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feadback feadback = db.Feadbacks.Find(id);
            if (feadback == null)
            {
                return HttpNotFound();
            }
            return View(feadback);
        }

        // GET: Feadbacks/Create
        public ActionResult Create()
        {
            ViewBag.Cus_Fid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName");
            ViewBag.Pro_Fid = new SelectList(db.Products, "Prod_Id", "Prod_Name");
            return View();
        }

        // POST: Feadbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feadback feadback)
        {
            if (ModelState.IsValid)
            {
                feadback.Cus_Fid = (int)Session["CID"];
                db.Feadbacks.Add(feadback);
                db.SaveChanges();
                return RedirectToAction("FeadBackDone", "Home");
            }

            ViewBag.Cus_Fid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName", feadback.Cus_Fid);
            ViewBag.Pro_Fid = new SelectList(db.Products, "Prod_Id", "Prod_Name", feadback.Pro_Fid);
            return View(feadback);
            
        }

        // GET: Feadbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feadback feadback = db.Feadbacks.Find(id);
            if (feadback == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cus_Fid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName", feadback.Cus_Fid);
            ViewBag.Pro_Fid = new SelectList(db.Products, "Prod_Id", "Prod_Name", feadback.Pro_Fid);
            return View(feadback);
        }

        // POST: Feadbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeadBack_ID,Cus_Fid,Pro_Fid,FeadBack_Comments")] Feadback feadback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feadback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cus_Fid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName", feadback.Cus_Fid);
            ViewBag.Pro_Fid = new SelectList(db.Products, "Prod_Id", "Prod_Name", feadback.Pro_Fid);
            return View(feadback);
        }

        // GET: Feadbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feadback feadback = db.Feadbacks.Find(id);
            if (feadback == null)
            {
                return HttpNotFound();
            }
            return View(feadback);
        }

        // POST: Feadbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feadback feadback = db.Feadbacks.Find(id);
            db.Feadbacks.Remove(feadback);
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
