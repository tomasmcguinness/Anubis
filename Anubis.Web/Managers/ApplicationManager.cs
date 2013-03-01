using Anubis.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Web.Managers
{
    public class ApplicationManager
    {
        public List<Application> GetApplications()
        {
            using (var ctx = new AnubisContext())
            {
                return ctx.Applications.ToList();
            }
        }

        public void CreateApplication(string applicationName)
        {
            using (var ctx = new AnubisContext())
            {
                ctx.Applications.Add(new Application() { Name = applicationName });
                ctx.SaveChanges();
            }
        }
    }
}