using Anubis.Web.Entities;
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
    public class LogController : Controller
    {
        private ApplicationManager appManager = null;
        private LogManager logManager = null;

        public LogController()
        {
            appManager = new ApplicationManager();
            logManager = new LogManager();
        }

        public ActionResult Index(long applicationId, int regionId)
        {
            var app = appManager.GetApplication(applicationId);
            List<LogEntity> messages = logManager.GetLogMessages(WebSecurity.CurrentUserId, app.Code, regionId);

            ApplicationLogModel model = new ApplicationLogModel();
            model.ApplicationName = app.Name;
            model.RegionName = ApplicationManager.GetRegionName(regionId);

            foreach (var message in messages)
            {
                model.LogMessages.Add(new LogMessageModel()
                {
                    LogLevel = message.LogLevel,
                    Message = message.Message,
                    Timestamp = message.Timestamp
                });
            }

            return View(model);
        }
    }
}
