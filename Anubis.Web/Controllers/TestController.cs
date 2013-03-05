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
        public ActionResult Index(string message)
        {
          log.Trace(message);
          return View();
        }
    }
}
