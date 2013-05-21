using Anubis.Client.Models;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingCentral
{
    public class TraceHandler
    {
        public void SendTraceRecord(string level, string message, string stackTrace, DateTime dateTime)
        {
            string loggingCode = ConfigurationManager.AppSettings["LoggingCentralCode"];

            if (loggingCode == null || loggingCode.Length == 0)
            {
                throw new InvalidOperationException("You must specify the LoggingCentralCode");
            }

            SendTraceRecord(loggingCode, level, message, stackTrace, dateTime);
        }

        public void SendTraceRecord(string code, string level, string message, string stackTrace, DateTime dateTime)
        {
            Task.Factory.StartNew(() => PostLogToAnubis(code, level, message, stackTrace, dateTime.Ticks));
        }

        private async Task PostLogToAnubis(string code, string level, string message, string stackTrace, long ticks)
        {
            HttpClient client = new HttpClient();

            string url = string.Format("http://anubis-we.azurewebsites.net/api/tracing/{0}", code); // TODO This target depends on where the client is installed.
            //string url = string.Format("http://localhost:82/api/tracing/{0}", code); // TODO This target depends on where the client is installed.

            LogModel model = new LogModel()
            {
                Ticks = ticks,
                Level = level,
                Message = message,
                StackTrace = stackTrace
            };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync<LogModel>(url, model);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // What do we do here???
                }
            }
            catch (Exception exp)
            {

            }
        }
    }
}
