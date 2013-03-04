using Anubis.Web.Filters;
using Anubis.Web.Managers;
using Anubis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Anubis.Web.Controllers
{
    [InitializeSimpleMembership]
    public class ApplicationsController : Controller
    {
        public ActionResult Index()
        {
            ApplicationManager manager = new ApplicationManager();
            var apps = manager.GetApplications();

            var appModels = apps.Select(s => new ApplicationModel() { ApplicationId = s.ApplicationId, Name = s.Name, Code = s.Code }).ToList();

            return View(appModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationManager manager = new ApplicationManager();
                manager.CreateApplication(WebSecurity.CurrentUserId, model.ApplicationName);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
