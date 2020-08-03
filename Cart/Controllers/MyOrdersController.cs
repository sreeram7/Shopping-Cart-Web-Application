using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Cart.Models;
using Cart.DataBase;


namespace Cart.Controllers
{
    public class MyOrdersController : Controller
    {
        
        // GET: MyOrders
        public ActionResult MyOrders()
        {
           
            List<Orders> order1 = Data_Orders.ordersList_method();  

            ViewData["list"] = order1;

            string sessionId = Data_Session.GetSessionId();

            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
                C.Open();
                string query = @"SELECT * FROM User_Info
                    WHERE SessionId= '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(query, C);

                SqlDataReader reader = cmd.ExecuteReader();

                 
                while (reader.Read())
                {
                   var userId =(int)reader[0];
                  
                    ViewData["Userid"] = userId;
                    
                }

            }


            return View();
        }

        public ActionResult ManageActivation()
        {
            // Code for manage activation codes method
            return View();
        }
    }
}   
   