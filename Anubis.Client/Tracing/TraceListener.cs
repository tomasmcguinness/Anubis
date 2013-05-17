using Anubis.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingCentral.Clients.Trace
{
    public class LoggingCentralTraceListener : System.Diagnostics.TraceListener
    {
        private TraceHandler handler;
        private const string DEFAULT_LOGGGING_LEVEL = "info";

        public LoggingCentralTraceListener()
        {
            this.handler = new TraceHandler();
        }

        public override void Write(string message)
        {
            handler.SendTraceRecord(DEFAULT_LOGGGING_LEVEL, message, null, DateTime.UtcNow);
        }

        public override void WriteLine(string message)
        {
            handler.SendTraceRecord(DEFAULT_LOGGGING_LEVEL, message, null, DateTime.UtcNow);
        }

        public override void Write(string message, string category)
        {
            handler.SendTraceRecord(category, message, null, DateTime.UtcNow);
        }

        public override void WriteLine(string message, string category)
        {
            handler.SendTraceRecord(category, message, null, DateTime.UtcNow);
        }
    }
}
