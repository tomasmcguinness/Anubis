using Anubis.Web.Data;
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
    public class ApplicationController : Controller
    {
        private ApplicationManager manager;
        private LogManager logManager;

        public ApplicationController()
        {
            manager = new ApplicationManager();
            logManager = new LogManager();
        }

        public ActionResult Index(long applicationId)
        {
            Application app = manager.GetApplication(applicationId);

            ApplicationModel model = new ApplicationModel()
            {
                Name = app.Name,
                Code = app.Code
            };

            ViewBag.Count = logManager.GetLogMessageCount(1, "Anubis", null);

            return View(model);
        }

        public ActionResult MonitorRegion(long applicationId, int selectedRegion)
        {
            manager.AddRegionToApplication(WebSecurity.CurrentUserId, applicationId, selectedRegion);
            return RedirectToAction("Index", new { applicationId = applicationId });
        }

        public int GetNumberOfMessagesForDataCentre()
        {
            return 0;
        }
    }
}