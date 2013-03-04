using Anubis.Web.Data;
using Anubis.Web.Managers;
using Anubis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anubis.Web.Controllers
{
    public class ApplicationController : Controller
    {
        private ApplicationManager manager;

        public ApplicationController()
        {
            manager = new ApplicationManager();
        }

        public ActionResult Index(long applicationId)
        {
            Application app = manager.GetApplication(applicationId);

            ApplicationModel model = new ApplicationModel()
            {
                Name = app.Name,
                Code = app.Code
            };

            return View(model);
        }

        public int GetNumberOfMessagesForDataCentre()
        {
            return 0;
        }
    }
}