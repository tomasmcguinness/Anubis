using Anubis.Web.Entities;
using Anubis.Web.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anubis.Web.Controllers
{
    public class LogController : Controller
    {
        private LogManager manager = null;

        public LogController()
        {
            manager = new LogManager();
        }

        public ActionResult Index(long applicationId, long regionId)
        {
            List<LogEntity> messages = manager.GetLogMessages("Anubis", regionId);
            return View(messages);
        }
    }
}
