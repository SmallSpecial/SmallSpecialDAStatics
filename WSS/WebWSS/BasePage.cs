using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
namespace WebWSS
{
    public class BasePage:Page
    {
        public string DateTimeFormat = System.Configuration.ConfigurationManager.AppSettings["DateTimeFormat"]; 
    }
}