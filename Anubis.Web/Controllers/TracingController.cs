using Anubis.Data;
using Anubis.Web.Managers;
using Anubis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anubis.Web.Controllers
{
    public class TracingController : ApiController
    {
        private LogManager logManager = null;
        private ApplicationManager appManager = null;

        public TracingController()
        {
            logManager = new LogManager();
            appManager = new ApplicationManager();
        }

        public HttpResponseMessage Post(string code, LogModel log)
        {
            Application app = appManager.GetUserAccountForCode(code);
            logManager.RecordLogMessage(app, log);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
