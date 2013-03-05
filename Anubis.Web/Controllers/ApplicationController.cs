using Anubis.Data;
using Anubis.Web.Data;
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
    [Authorize]
    public class ApplicationController : Controller
    {
        private ApplicationManager appManager;
        private LogManager logManager;

        public ApplicationController()
        {
            appManager = new ApplicationManager();
            logManager = new LogManager();
        }

        public ActionResult Index(long applicationId)
        {
            Application app = appManager.GetApplication(applicationId);

            ApplicationModel model = new ApplicationModel()
            {
                ApplicationId = app.ApplicationId,
                Name = app.Name,
                Code = app.Code
            };

            return View(model);
        }

        public ActionResult Regions(long applicationId)
        {
            List<ApplicationRegion> regions = appManager.GetApplicationRegions(WebSecurity.CurrentUserId, applicationId);

            List<ApplicationRegionModel> model = new List<ApplicationRegionModel>();

            foreach (var region in regions)
            {
                model.Add(new ApplicationRegionModel() { Name = ApplicationManager.GetRegionName(region.RegionId), RegionId = region.RegionId });
            }

            return View(model);
        }

        public ActionResult MonitorRegion(long applicationId, int selectedRegion)
        {
            appManager.AddRegionToApplication(WebSecurity.CurrentUserId, applicationId, selectedRegion);
            return RedirectToAction("Index", new { applicationId = applicationId });
        }

        [HttpPost]
        public int GetNumberOfMessagesForRegion(long applicationId, int regionId)
        {
            var app = appManager.GetApplication(applicationId);
            var messageCount = logManager.GetLogMessageCount(WebSecurity.CurrentUserId, app.Code, regionId);
            return messageCount;
        }
    }
}