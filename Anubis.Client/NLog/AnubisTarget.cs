using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Client.NLog
{
    [Target("AnubisTarget")]
    public class AnubisTarget : TargetWithLayout
    {
        private TraceHandler handler;

        public AnubisTarget()
        {
            this.handler = new TraceHandler();
        }

        public string Code { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            SendTheMessageToRemoteHost(this.Code, logEvent);
        }

        private void SendTheMessageToRemoteHost(string code, LogEventInfo logEvent)
        {
            string logMessage = this.Layout.Render(logEvent);
            string stackTrace = logEvent.Exception != null ? BuildStackTrace(logEvent.Exception) : null;
            handler.SendTraceRecord(code, logEvent.Level.ToString().ToLower(), logMessage, stackTrace);
        }

        private string BuildStackTrace(Exception exp)
        {
            return string.Format("{0} - [{1}] - {2}", exp.GetType().ToString(), exp.Message, exp.StackTrace);
        }
    }
}
