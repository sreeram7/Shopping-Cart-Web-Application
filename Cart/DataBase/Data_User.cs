using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Cart.Models;
using Cart.DataBase;
using System.Diagnostics;
using System.Web.Configuration;

namespace Cart.DataBase
{
    
    public class Data_User: Datalink
    {
         public static Customers GetUserInfo(string username) 
         {
            Customers u_info = new Customers();
        
            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
                C.Open();

                string query = @"SELECT * from User_Info
                    WHERE Username = '" + username + "'";    
                SqlCommand cmd = new SqlCommand(query, C);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    u_info = new Customers();
                    u_info.Id = (int)reader["UserId"];
                    u_info.Username = (string)reader["Username"];
                    u_info.Password = (string)reader["Password"];
                    
                }
            }
            return u_info;   //pass to calling function (Login controller)
         }
    }
}