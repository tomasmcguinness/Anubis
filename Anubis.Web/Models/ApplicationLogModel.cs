using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Web.Models
{
    public class ApplicationLogModel
    {
        public ApplicationLogModel()
        {
            LogMessages = new List<LogMessageModel>();
        }

        public string ApplicationName { get; set; }
        public string RegionName { get; set; }
        public List<LogMessageModel> LogMessages { get; private set; }
    }
}