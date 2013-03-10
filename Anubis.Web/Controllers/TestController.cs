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
                    TestMethodOne();
                }
                catch (Exception exp)
                {
                    log.ErrorException(message, exp);
                }
            }

            return View();
        }

        public void TestMethodOne()
        {
            TestMethodTwo();
        }

        private void TestMethodTwo()
        {
            throw new InvalidOperationException("From TestMethodTwo");
        }

    }
}
