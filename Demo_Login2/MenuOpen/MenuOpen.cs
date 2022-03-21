using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.MenuOpen
{
    public static class MenuOpen
    {
        public static string IsActive(this HtmlHelper html,string controller = null)
        {
            const string cssClass = "active";
            var currentController = (string)html.ViewContext.RouteData.Values["Controller"];
            if (String.IsNullOrEmpty(controller))
            {
                controller = currentController;
            }
            return controller == currentController ? cssClass : String.Empty;
        }

        public static string IsMenuOpen(this HtmlHelper html, string controller = null)
        {
            const string cssClass = "menu-open";
            var currentController = (string)html.ViewContext.RouteData.Values["Controller"];
            if (String.IsNullOrEmpty(controller))
            {
                controller = currentController;
            }
            return controller == currentController ? cssClass : String.Empty;
        }


    }
}