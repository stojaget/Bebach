using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BebachModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Bebach.Models;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Bebach.ViewModels;
using System.Web.Routing;


namespace Bebach.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private Entities db = new Entities();
        // GET: /Report/  invoked for the first time on page load of Report form
        [AllowAnonymous]
        public ActionResult ReportViewer(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// invoked when form post takes place due to button clicked event
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ViewResult ReportViewer(SearchParameterModel um)
        {
            return View(um);
        }
       // [Authorize]
        public ActionResult Index()
        {
           // var userId = User.Identity.GetUserId();
            //var userManager = User.Identity.GetUserName().ToString();
            //var roleManager = UserManager.GetRoles(userId);

          
            //IQueryable<int> bebe = db.Bebas.Where(x => x.Unio.Trim().ToString() == userManager.Trim().ToString()).Select(x => x.ID);

            return Redirect("~/Reports/Izvjesca.aspx");
        }

    }
}