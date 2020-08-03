using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Cart.Models;
using Cart.DataBase;
using System.Diagnostics;
using System.Text.RegularExpressions;

using System.Text;
using System.Security.Cryptography;

namespace Cart.Controllers
{
    public class LoginController : Controller
    {
      
        public ActionResult Login(string Username, string Password)
        {
            if (Username == null || Password == null)
                return View(); //display home screen 

            else
            {
                string Hash_Password = GetMD5Hash(Password);
                Debug.WriteLine(Hash_Password);
                Customers user = Data_User.GetUserInfo(Username);

                if (user == null || user.Password != Hash_Password)
                    return View(); //display home screen 

                 string  sessionId = Data_Session.NewSession(user.Id); //Start new session
                return RedirectToAction("Search", "Gallery", new { uid = user.Id, username = Username, ses_id = sessionId });
            }
        }
        public static string GetMD5Hash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password)); //Compute hash from the bytes of text
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));  //Each byte is changed into 2-hexadecimal digits  
            }
            return strBuilder.ToString();
        }

        [HttpPost]      
        public JsonResult IsChecked(string Username)
        {
            if (Regex.IsMatch(Username.ToString(), "^[a-zA-Z0-9]+$"))
                return Json(true);
            return Json(false);
        }
        public ActionResult Logout(string sessionId)
        {
            Data_Session.DeleteSession(sessionId);
            return RedirectToAction("Login", "Login");
        } 
    }
}