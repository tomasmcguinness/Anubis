using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Anubis.Data;

namespace Anubis.Web.Data
{
    public class AnubisContext : DbContext
    {
        public IDbSet<Application> Applications { get; set; }
        public IDbSet<ApplicationRegion> ApplicationRegions { get; set; }
    }
}