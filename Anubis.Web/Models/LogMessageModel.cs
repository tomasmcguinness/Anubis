using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Web.Models
{
    public class LogMessageModel
    {
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
        public long Ticks { get; set; }
    }
}