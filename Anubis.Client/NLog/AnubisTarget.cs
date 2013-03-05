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
      string logMessage = this.Layout.Render(logEvent);
      Task t = SendTheMessageToRemoteHost(this.Code, logEvent.Level, logMessage);
      t.Wait();
    }

    private async Task SendTheMessageToRemoteHost(string code, LogLevel level, string message)
    {
      Task t = handler.SendTraceRecord(code, level.ToString().ToLower(), message);
      await t;
    }
  }
}
