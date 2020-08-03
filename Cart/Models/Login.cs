using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Cart.Controllers;


namespace Cart.Models
{
    public class Login
    {
        [Remote("IsChecked", "Login", HttpMethod = "POST", ErrorMessage = " special characters not allowed")]
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username:")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }    
    }
}