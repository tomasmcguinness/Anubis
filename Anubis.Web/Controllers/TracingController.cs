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
        private UserManager userManager = null;

        public TracingController()
        {
            logManager = new LogManager();
            userManager = new UserManager();
        }

        public HttpResponseMessage Post(string code, string application, LogModel log)
        {
            int userId = userManager.GetUserAccountForCode(code);
            logManager.RecordLogMessage(userId, application, log);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Post(string code, LogModel log)
        {
            int userId = userManager.GetUserAccountForCode(code);
            logManager.RecordLogMessage(userId, log);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
