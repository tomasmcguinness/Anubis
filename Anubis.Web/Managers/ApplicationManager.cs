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

        public void CreateApplication(int ownerId, string applicationName)
        {
            using (var ctx = new AnubisContext())
            {
                ctx.Applications.Add(new Application() { Name = applicationName, Code = GetRandomApplicationCode(ownerId), OwnerId = ownerId });
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
                app.Regions.Add(new ApplicationRegion() { RegionId = selectedRegion, Application = app });
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

        public static string GetRegionName(int p)
        {
          switch (p)
          {
            case 10:
              return "West US";
            case 20:
              return "East US";
            case 30:
              return "East Asia";
            case 40:
              return "North Europe";
            case 50:
              return "West Europe";
            default:
              return "Unknown";
          }
        }

        public Application GetUserAccountForCode(string code)
        {
            using (var ctx = new AnubisContext())
            {
                var app = ctx.Applications.Where(a => a.Code == code).SingleOrDefault();
                return app;
            }
          }
    }
}