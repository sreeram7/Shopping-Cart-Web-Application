using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Cart.Models;
using System.Diagnostics;
using Cart.DataBase;


namespace S_cart.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
       // public static string connectionString = "Server=LAPTOP-NC95240P;" + "Database=DB_Cart;" + "Integrated Security=true";
        public ActionResult Search(int uid,string username,string sess_id)
        {
            Prod_List_class PLC_Obj = new Prod_List_class();
            List<Products> lst_products = PLC_Obj.Products_List();
            ViewData["list"] = lst_products;
            ViewData["user"] = username;
            ViewData["uid"] = uid;
            Session["uid"] = uid;
            Session.Abandon();

            int CountCartItems = PLC_Obj.Sum_Cart(uid);
            ViewData["count"] = CountCartItems;
            
            return View();

        }

        [HttpPost]
        public ActionResult Search(int count, int uid,string user, string search)
        {
            search = Request.Form["searchqueery"];
            List<Products> prod_Search = Prod_List_class.SearchItem(search);
            ViewData["list"] = prod_Search;
            ViewData["uid"] = uid;
            ViewData["count"] = count;
            ViewData["user"] = user;
            return View();
        }


        [HttpPost]
        public JsonResult AddtoCart(string id)
        {
            Debug.WriteLine(id);
            
            return Json("ok",JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add2Cart(int u_id, int p_id)  //this method triggers when the "ADD" button is pressed for the item.
        {
            using (SqlConnection conn = new SqlConnection(Datalink.connectionString))
            {
                conn.Open();
                string cmdtext = @"insert into Cart_Info (UserId,CartId,Product_ID,Status_Code) values ("+ "111" +"," + u_id + "," + p_id + ",0)";
                SqlCommand cmd = new SqlCommand(cmdtext, conn);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Search", new { uid = u_id });
        }

        public ActionResult RemovefromCart(int u_id, int p_id) //this method triggers when the "Remove" button is pressed for the item.
        {
            using (SqlConnection conn = new SqlConnection(Datalink.connectionString))
            {
                conn.Open();
                string cmdtext = @"delete from  Cart_Info where CartId in (select TOP 1 CartId from Cart_Info where UserId =" + u_id + " and Product_ID=" + p_id + "  order by CartId desc )";
                SqlCommand cmd = new SqlCommand(cmdtext, conn);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Search", new { uid = u_id });
        }

    }
}