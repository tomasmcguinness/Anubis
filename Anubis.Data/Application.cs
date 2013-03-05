using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Data
{
    public class Application
    {
        public long ApplicationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int OwnerId { get; set; }
        public virtual ICollection<ApplicationRegion> Regions { get; set; }
    }
}