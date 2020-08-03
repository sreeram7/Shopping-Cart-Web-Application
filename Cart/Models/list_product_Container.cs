using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Cart.Models;
using System.Diagnostics;
using Cart.DataBase;

namespace Cart.Models
{
    public class Prod_List_class : Datalink
    {
        
        public List<Products> Products_List()
        {
            List<Products> Lt_products = new List<Products>();

            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
                C.Open();

                string cmdtext = @"SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(cmdtext, C);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Products Pd = new Products();
                    Pd.Product_Name = (string)sdr["Product_Name"];
                    Pd.Product_Id = (int)sdr["Product_ID"];
                    Pd.Prod_Desc = (string)sdr["Prod_Desc"];
                    Pd.Url = (string)sdr["Url"];
                    Pd.Price= (int)sdr["Price"];

                    Lt_products.Add(Pd);
                 
                }
                return Lt_products;
            }

        }
        public int Sum_Cart(int user_id) 
    {
            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
                C.Open();

                string cmdtext = @"select count(*) as count from Cart_Info where UserId =" + user_id + "and status_code=0";
                SqlCommand cmd = new SqlCommand(cmdtext, C);
                int count = (int)cmd.ExecuteScalar();
                return count;
            }
        }
        public static List<Products> SearchItem(string search) 
        {

            List<Products> Prod_Search = new List<Products>();
            SqlConnection C1 = new SqlConnection(Datalink.connectionString);

            C1.Open();
            string cmdtext = @"select * from Products where Product_Name LIKE '%"+ search +"%'";
            SqlCommand cmd1 = new SqlCommand(cmdtext, C1);
            SqlDataReader reader = cmd1.ExecuteReader();

            
            while (reader.Read())
            {
                Products Pd1 = new Products();
                
                Pd1.Product_Id = (int)reader[0];
                Pd1.Product_Name = (string)reader[1];
                Pd1.Price = (int)reader[3];
                Pd1.Prod_Desc = (string)reader[2];
                Pd1.Quantity = (int)reader[4];
                Pd1.Url = (string)reader[5];
                
            Prod_Search.Add(Pd1);
            }
            C1.Close();
            return Prod_Search;
        }
    }
}