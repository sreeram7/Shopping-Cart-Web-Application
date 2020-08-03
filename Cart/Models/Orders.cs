using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Image { get; set; }
        public string Prod_Desc { get; set; }
        public int Quantity { get; set; }
        public string OrderDate { get; set; }
        public string Act_Code { get; set; }
    }
}