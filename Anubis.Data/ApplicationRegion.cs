using Anubis.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anubis.Data
{
    public class ApplicationRegion
    {
        public long ApplicationRegionId { get; set; }
        public int RegionId { get; set; }

        public virtual Application Application { get; set; }
    }
}
