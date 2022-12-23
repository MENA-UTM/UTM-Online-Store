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
    public class orderdetailsController : Controller
    {
        private OnlineShoppingEntities2 db = new OnlineShoppingEntities2();

        // GET: orderdetails
        public ActionResult Index()
        {
            var orderdetails = db.orderdetails.Include(o => o.order).Include(o => o.Product).Include(o => o.Customer);
            return View(orderdetails.ToList());
        }
        public ActionResult History()
        {
            int c = (int)Session["CID"];
            return View(db.orderdetails.Where(x=>x.CusFid==c).ToList());
        }

        // GET: orderdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // GET: orderdetails/Create
        public ActionResult Create()
        {
            ViewBag.order_fid = new SelectList(db.orders, "order_id", "order_status");
            ViewBag.product_fid = new SelectList(db.Products, "Prod_Id", "Prod_Name");
            ViewBag.CusFid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName");
            return View();
        }

        // POST: orderdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderdetail_id,order_fid,product_fid,psale_price,pp_price,quantity,order_feadback,CusFid")] orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.orderdetails.Add(orderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_fid = new SelectList(db.orders, "order_id", "order_status", orderdetail.order_fid);
            ViewBag.product_fid = new SelectList(db.Products, "Prod_Id", "Prod_Name", orderdetail.product_fid);
            ViewBag.CusFid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName", orderdetail.CusFid);
            return View(orderdetail);
        }

        // GET: orderdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_fid = new SelectList(db.orders, "order_id", "order_status", orderdetail.order_fid);
            ViewBag.product_fid = new SelectList(db.Products, "Prod_Id", "Prod_Name", orderdetail.product_fid);
            ViewBag.CusFid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName", orderdetail.CusFid);
            return View(orderdetail);
        }

        // POST: orderdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderdetail_id,order_fid,product_fid,psale_price,pp_price,quantity,order_feadback,CusFid")] orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_fid = new SelectList(db.orders, "order_id", "order_status", orderdetail.order_fid);
            ViewBag.product_fid = new SelectList(db.Products, "Prod_Id", "Prod_Name", orderdetail.product_fid);
            ViewBag.CusFid = new SelectList(db.Customers, "Customer_Id", "Customer_FirstName", orderdetail.CusFid);
            return View(orderdetail);
        }

        // GET: orderdetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: orderdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orderdetail orderdetail = db.orderdetails.Find(id);
            db.orderdetails.Remove(orderdetail);
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
