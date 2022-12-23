using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Models;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;

namespace OnlineShopping.Controllers
{
    
    public class HomeController : Controller
    {
        private OnlineShoppingEntities2 db = new OnlineShoppingEntities2();

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult History()
        {
            //int i = db.orderdetails.Where(x => x.CusFid == idc && x.product_fid == idp && x.order_feadback != true).Count();
            //Session["fshow"] = i;

            //int c = (int)Session["CID"];

            //return View(db.orderdetails.Where(x => x.CusFid == c).ToList());
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult FeadBack(int idp,int idc)
        {

            
            Session["CFeadID"] = idc;
            Session["FID"] = idp;

            // Customer customer = new Customer(); 

            // var id = db.orderdetails.Where(x => x.CusFid == customer.Customer_Id ).Select(v => v.product_fid).FirstOrDefault();

            //var i = db.orderdetails.Where(x => x.CusFid == (int)Session["CID"] && x.product_fid ==id && x.order_feadback==true).Count();


            return View();
        }

        public ActionResult testimonial()
        {
            return View();
        }
        //[Authorize(Users = @"zobair")]
        public ActionResult Products(int? id)
        {

            ProductModel p = new ProductModel();
            p.cat = db.Categories.ToList();

            if(id==null)
            {
                p.pro = db.Products.ToList();
            }
            else
            {
                p.pro = db.Products.Where(z => z.Cat_Fid == id).ToList();
            }
            return View(p);
        }
        public ActionResult blog()
        {
            return View();
        }
        public ActionResult cart()
        {
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            List<Product> list;
            if (Session["myCart"] == null)
            {
                list = new List<Product>();
            }
            else
            {
                list = (List<Product>)Session["myCart"];
            }
            list.Add(db.Products.Where(p => p.Prod_Id == id).FirstOrDefault());
            list[list.Count - 1].PRO_QUANTITY = 1;


            Session["myCart"] = list;
            return RedirectToAction("cart");
        }
        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].PRO_QUANTITY--;
        
            Session["myCart"] = list;
            return RedirectToAction("cart");
        }

        public ActionResult PlusToCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            
            
                list[RowNo].PRO_QUANTITY++;
            Session["myCart"] = list;
            return RedirectToAction("cart");
        }

        public ActionResult RemoveFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("cart");
        }

        public ActionResult FeadBackDone()
        {
            return View();
        }
        

         public ActionResult Reviews(int id)
        {


            return View(db.Feadbacks.Where(z => z.Pro_Fid == id).ToList());
        }
        public ActionResult paynow()
        {
          



            if (Session["ID"] != null)
            {
                order o = new order();

                o.order_status = "paid";
                o.order_date = DateTime.Now;
                o.cus_fid = (int)Session["CID"];

                
                //db.orders.Add(o);
                //db.SaveChanges();
                Session["Order"] = o;


                return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=ranazubair321@gmail.com&item_name=FoodFun&return=https://localhost:44338/Home/OrderBooked&amount=" + double.Parse(Session["totalAmountS"].ToString()) / 220);

            }
            else
            {
                return RedirectToAction("LogIn", "Customers");
            }


        }

        public ActionResult OrderBooked()
        {

            
            order o = (order)Session["Order"];
            //Customer customer = new Customer();

            //customer = (Customer)Session["CIDD"];
            //string to = customer.Customer_Email;
            //string from = "rana.zubair971@gmail.com";
            //MailMessage message = new MailMessage(from, to);
            //string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            //message.Subject = "Sending Email Using Asp.Net & C#";
            //message.Body = mailbody;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true;
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            //System.Net.NetworkCredential basicCredential1 = new
            //System.Net.NetworkCredential("rana.zubair971@gmail.com", "ZZubairpugc4545438");
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;
            //try
            //{
            //    client.Send(message);
            //}

            //catch (Exception ex)
            //{
            //    throw ex;
            //}
         
            //MailMessage mail = new MailMessage();


            //mail.From = new MailAddress("zubair@technors.net");
            //mail.To.Add(customer.Customer_Email);
            //mail.Subject = "Order Confirmation";
            //mail.Body = "<b>FoodFun!</b><br> Thanks for your Order. we will delivery your order within 48 hours";
            //mail.IsBodyHtml = true;


            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //SmtpServer.Port = 587;
            //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("zubair@technors.net", "@ZZubairpugc4545438");
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);



            db.orders.Add(o);
            db.SaveChanges();
            List<Product> p = (List<Product>)Session["myCart"];
            for (int i = 0; i < p.Count; i++)
            {
                orderdetail od = new orderdetail();

                int orderID = db.orders.Max(x => x.order_id);
                od.order_fid = orderID;
                od.product_fid = p[i].Prod_Id;
                od.quantity = p[i].PRO_QUANTITY * -1;
                od.pp_price = Convert.ToDecimal(p[i].Purchase_Price);
                od.psale_price = Convert.ToDecimal(p[i].Sale_Price);
                od.CusFid = (int)Session["CID"];

                db.orderdetails.Add(od);
                db.SaveChanges();
            }
            return View();

        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}