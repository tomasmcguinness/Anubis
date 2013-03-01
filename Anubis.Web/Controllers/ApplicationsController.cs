using Anubis.Web.Managers;
using Anubis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anubis.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        public ActionResult Index()
        {
            ApplicationManager manager = new ApplicationManager();
            var apps = manager.GetApplications();

            var appModels = apps.Select(s => new ApplicationModel() { Name = s.Name }).ToList();

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
                manager.CreateApplication(model.ApplicationName);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
