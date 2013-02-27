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
      this.Host = "localhost";
    }

    public string Host { get; set; }

    protected override void Write(LogEventInfo logEvent)
    {
      string logMessage = this.Layout.Render(logEvent);
      Task t = SendTheMessageToRemoteHost(this.Host, logEvent.Level, logMessage);
      t.Wait();
    }

    private async Task SendTheMessageToRemoteHost(string host, LogLevel level, string message)
    {
      Task t = handler.SendTraceRecord(level.ToString().ToLower(), message);
      await t;
    }
  }
}
