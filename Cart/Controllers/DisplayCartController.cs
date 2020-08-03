using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cart.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using Cart.DataBase;

namespace Cart.Controllers
{
    public class DisplayCartController : Controller
    {
        //public static string connectionString = "Server=LAPTOP-NC95240P;" + "Database=DB_Cart;" + "Integrated Security=true";
        // GET: ViewCart
        public ActionResult DisplayCart(int u_id, string user_name)
        {
            Cart_List_IntClass CLI_obj = new Cart_List_IntClass();
            List<MyCart> mc = CLI_obj.lst_mycart(u_id);
            ViewData["list"] = mc;
            ViewData["uid"] = u_id;
            ViewData["user"]= user_name;

            using (SqlConnection connection = new SqlConnection(Datalink.connectionString))
            {
                connection.Open();

                string cmdtext = @"select count(*) as count from Cart_Info where UserId =" + u_id + "and Status_Code=0";
                SqlCommand cmd = new SqlCommand(cmdtext, connection);
                int cart_count = (int)cmd.ExecuteScalar();
                ViewData["count"] = cart_count;
            }
            return View();
        }

        public ActionResult RemoveFromCart(int u_id, int p_id) 
        {
            using (SqlConnection connection = new SqlConnection(Datalink.connectionString))
            {
                connection.Open();
                string cmdtext = @"delete from Cart_Info where Product_ID=" + p_id + " and UserId=" + u_id;
                SqlCommand cmd = new SqlCommand(cmdtext, connection);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("DisplayCart", new { u_id = u_id });
        }


     
    }

    internal class Cart_List_IntClass
    {
        public static string connectionString = "Server=LAPTOP-NC95240P;" + "Database=DB_Cart;" + "Integrated Security=true";
        public List<MyCart> lst_mycart(int u_id)
        {
            List<MyCart> lst_mycart = new List<MyCart>();

            using (SqlConnection connection = new SqlConnection(Datalink.connectionString))
            {
                connection.Open();
                string cmdtext = @"select P.Product_ID, count(P.Product_ID) as count,P.Product_Name,P.Url,P.Price from Cart_Info C join Products P on C.Product_Id = P.Product_ID where C.UserId = " + u_id+ " and c.status_code = 0 group by P.Product_ID,P.Product_Name,P.Url,P.Price";
                SqlCommand cmd = new SqlCommand(cmdtext, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MyCart mc = new MyCart();
                    mc.Product_Name = (string)reader["Product_Name"];
                    mc.Product_Id = (int)reader["Product_ID"];
                    mc.Url = (string)reader["Url"];
                    mc.Price = (int)reader["Price"];
                    mc.Quantity = (int)reader["count"];

                    lst_mycart.Add(mc);

                    Debug.WriteLine(reader["Product_ID"]);
                    Debug.WriteLine(reader["Product_Name"]);
                    Debug.WriteLine(reader["Url"]);
                    Debug.WriteLine(reader["Price"]);
                    Debug.WriteLine(reader["count"]);
                }
            }
            return lst_mycart;
        }
       
    }
}