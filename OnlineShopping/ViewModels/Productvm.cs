using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.ViewModels
{
    public class Productvm
    {
        public int Prod_Id { get; set; }
        public string Prod_Name { get; set; }
        public string Prod_Disc { get; set; }
        public string Purchase_Price { get; set; }
        public string Sale_Price { get; set; }
        public string Prod_Pic { get; set; }
       
        public HttpPostedFileBase Pro_Pic { get; set; }
        
        public int PRO_QUANTITY { get; set; }
        public int Cat_Fid { get; set; }
    }
}