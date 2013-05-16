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
using System.Threading.Tasks;

namespace LoggingCentral
{
    public class TraceHandler
    {
        private static string connectionString = "Endpoint=sb://anubis.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=0P3oVJss/4SrHYVgr6SdsLUgjzH9wXec44ODUJtZwWo=";
        private static TopicClient client = TopicClient.CreateFromConnectionString(connectionString, "errorlogcollectiontopic");

        public void SendTraceRecord(string level, string message, string stackTrace)
        {
            string loggingCode = ConfigurationManager.AppSettings["LoggingCentralCode"];

            if (loggingCode == null || loggingCode.Length == 0)
            {
                throw new InvalidOperationException("You must specify the LoggingCentralCode");
            }

            SendTraceRecord(loggingCode, level, message, stackTrace);
        }

        public void SendTraceRecord(string code, string level, string message, string stackTrace)
        {
            Task postTask = PostLogToAnubis(code, level, message, stackTrace);

            Task.WaitAll(postTask);
        }

        //private async Task SendAlarmToAnubis(string code, string level, string message, string strackTrace)
        //{
        //    LogModel model = new LogModel()
        //    {
        //        Level = level,
        //        Message = message,
        //        StackTrace = strackTrace
        //    };

        //    try
        //    {
        //        BrokeredMessage bm = new BrokeredMessage(model);
        //        bm.Label = "ErrorLogMessage";

        //        client.Send(bm);
        //    }
        //    catch
        //    {
        //        // NO OP
        //    }
        //}

        private async Task PostLogToAnubis(string code, string level, string message, string stackTrace)
        {
            HttpClient client = new HttpClient();

            string url = string.Format("http://anubis-we.azurewebsites.net/api/tracing/{0}", code); // TODO This target depends on where the client is installed.
            //string url = string.Format("http://localhost:82/api/tracing/{0}", code); // TODO This target depends on where the client is installed.

            LogModel model = new LogModel()
            {
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
