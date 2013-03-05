using Anubis.Data;
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

        public void CreateApplication(int userId, string applicationName)
        {
            using (var ctx = new AnubisContext())
            {
                ctx.Applications.Add(new Application() { Name = applicationName, Code = GetRandomApplicationCode(userId) });
                ctx.SaveChanges();
            }
        }

        private string GetRandomApplicationCode(int userId)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            result = string.Format("{0}{1}", userId, result);

            return result;
        }

        private Application GetApplication(int ownerId, long applicationId)
        {
            using (var ctx = new AnubisContext())
            {
                return ctx.Applications.Where(a => a.ApplicationId == applicationId && a.OwnerId == ownerId).Single();
            }
        }

        public Application GetApplication(long applicationId)
        {
            using (var ctx = new AnubisContext())
            {
                return ctx.Applications.Find(applicationId);
            }
        }

        public void AddRegionToApplication(int ownerId, long applicationId, int selectedRegion)
        {
            ApplicationRegion region = new ApplicationRegion();
            using (var ctx = new AnubisContext())
            {
                var app = ctx.Applications.Find(applicationId);
                app.Regions.Add(new ApplicationRegion() { ApplicationRegionId = selectedRegion, Application = app });
                ctx.SaveChanges();
            }
        }

        public List<ApplicationRegion> GetApplicationRegions(int ownerId, long applicationId)
        {
            using (var ctx = new AnubisContext())
            {
                var app = ctx.Applications.Find(applicationId);

                if (app.Regions != null)
                {
                    return app.Regions.ToList();
                }

                return null;
            }
        }
    }
}