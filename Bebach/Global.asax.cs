using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Bebach.Extensions;
using System.Globalization;
using System.Web.Security;
using Bebach.Controllers;

namespace Bebach
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DateTime?), new MyDateTimeModelBinder());
           ModelBinders.Binders.Add(typeof(DateTime), new TimeBinder());
            Bebach.App_Start.CustomModelBinderConfig.RegisterCustomModelBinders();
            // Database.SetInitializer<ApplicationDbContext>(null);
            // Database.SetInitializer<ApplicationDbContext>(new ApplicationDbContext());
        }
        protected void Application_BeginRequest()
        {
            //CultureInfo cInf = new CultureInfo("hr-HR", false);
           

            //cInf.DateTimeFormat.DateSeparator = "/";
            //cInf.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            //cInf.DateTimeFormat.LongDatePattern = "dd/MM/yyyy hh:mm:ss tt";

            //System.Threading.Thread.CurrentThread.CurrentCulture = cInf;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = cInf;
        }

        //protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        //{
        //    // Get the authentication cookie
        //    string cookieName = FormsAuthentication.FormsCookieName;
        //    HttpCookie authCookie = Context.Request.Cookies[cookieName];

        //    // If the cookie can't be found, don't issue the ticket, redirect to the login page.
        //    if (authCookie == null)
        //    {
        //        var routeData = new RouteData();
        //        routeData.Values["controller"] = "Account";
        //        routeData.Values["action"] = "Login";
        //        IController controller = null;
        //        controller = new AccountController();
        //        ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(HttpContext.Current), routeData));
        //    }
        //}
    }
}
