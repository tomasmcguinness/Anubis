using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anubis.Web.Controllers
{
    public class TestController : Controller
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            log.Trace("Returning Index");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string level, string message)
        {
            if (level == "trace")
            {
                log.Trace(message);
            }
            else
            {
                try
                {
                    throw new InvalidCastException("Invalid cast for testing");
                }
                catch (Exception exp)
                {
                    log.Error(message, exp);
                }
            }

            return View();
        }
    }
}
