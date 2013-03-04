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

        public Application GetApplication(long applicationId)
        {
            using (var ctx = new AnubisContext())
            {
                return ctx.Applications.Find(applicationId);
            }
        }
    }
}