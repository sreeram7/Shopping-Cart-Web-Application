using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Cart.DataBase
{
    // Handle all queries pertaining to Session
    
public class Data_Session: Datalink
    {
        
        static string session_Id;
        public static bool IsSessionIdActive(string sessionId) 
        {
            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
                C.Open();
                string query = @"SELECT COUNT(*) FROM User_Info
                WHERE SessionId= '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(query, C);
                int count = (int)cmd.ExecuteScalar();
                return (count == 1);
            }
        }

        public static string NewSession(int userId) //Ceate session  to  NewSession
        {
            string sessionId = Guid.NewGuid().ToString();
            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
               C.Open();
                string query = @"UPDATE User_Info SET SessionId = '" + sessionId + "'" +
                        " WHERE userId =" + userId;
                SqlCommand cmd = new SqlCommand(query, C);
                cmd.ExecuteNonQuery();
            }
            return sessionId;
        }

        public static void DeleteSession(string sessionId) //To delete session
        {
            
            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
                C.Open();
                string sql = @"UPDATE User_Info SET SessionId = NULL 
                    WHERE SessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, C);
                cmd.ExecuteNonQuery();
            }
        }

        public static string GetSessionId() //To geta session ID
        {
            using (SqlConnection C = new SqlConnection(Datalink.connectionString))
            {
                C.Open();
                string query = @"SELECT * FROM User_Info";
                SqlCommand cmd = new SqlCommand(query, C);
                SqlDataReader reader = cmd.ExecuteReader();

                var temp = reader.Read();
                    string S_Id = (string)reader[3];
                return S_Id;
            }
        }
    }
}