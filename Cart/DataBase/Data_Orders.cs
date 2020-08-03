using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using Cart.Models;
using System.Web.Configuration;
using System.Diagnostics;
using Cart.DataBase;

namespace Cart.DataBase
{
    

    public class Data_Orders :Datalink
    {
        
        //Handle all queries relating to purchase
        public static List<Orders> ordersList_method()
        {
            string session_id = Data_Session.GetSessionId();
            List<Orders> orders1 = new List<Orders>();

            //Instantiate the connection
            SqlConnection connection = new SqlConnection(Datalink.connectionString);

            connection.Open();

            string querybyuserID = "select UserId from User_Info where SessionId = '" + session_id + "'"; 

            //Instantiate a new command with a query and connection
            string cmdtext = @"select CI.UserID, Pd.Product_ID, count(Pd.product_ID) as Quantity, Pd.Product_Name,Pd.Url,Pd.Prod_Desc,CI.Date, CI.Act_Code" +
                                            " from Cart_Info CI join Products Pd on CI.Product_ID = Pd.Product_ID" +
                                            " where CI.UserId = (" + querybyuserID + ") and CI.Status_Code = 1" +
                                            " group by CI.UserId, Pd.Product_ID, Pd.Product_Name,Pd.Url,Pd.Prod_desc,CI.Date,CI.Act_Code" +
                                            " order by CI.UserId, Pd.Product_ID, Pd.Product_Name,Pd.Url,Pd.Prod_desc,CI.Date,CI.Act_Code";
            SqlCommand cmd = new SqlCommand(cmdtext, connection);

            //Call Execute reader to get query results
            SqlDataReader reader = cmd.ExecuteReader();

            //Print out each record
            while (reader.Read())
            {
                Orders ord = new Orders();
                {
                    ord.Id = (int)reader["Product_ID"];
                    ord.Quantity = (int)reader["Quantity"];
                    ord.Product_Name = (string)reader["Product_Name"];
                    ord.Image = (string)reader["Url"];
                    ord.Prod_Desc = (string)reader["Prod_Desc"];
                    ord.OrderDate = (string)reader["Date"];
                    ord.Act_Code = (string)reader["Act_Code"];
                }
                orders1.Add(ord);
                Debug.WriteLine(reader["Product_ID"]);
            }

            return orders1;
        }
    }
}