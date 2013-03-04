using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Anubis.Web.Data
{
    public class AnubisContext : DbContext
    {
        public IDbSet<Application> Applications { get; set; }
    }
}