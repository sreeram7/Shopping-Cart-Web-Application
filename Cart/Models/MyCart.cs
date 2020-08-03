using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart.Models
{
    public class MyCart
    {
        public int Cart_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Url { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Status_Code { get; set; }
        public string Act_Code { get; set; }

    }
}