using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart.Models
{
    public class Products
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public int Price { get; set; }
        public string Prod_Desc { get; set; }
        public int Quantity { get; set; }
        public string Url { get; set; }
    }
}