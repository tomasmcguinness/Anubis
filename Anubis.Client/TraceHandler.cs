using Anubis.Client.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Client
{
  internal class TraceHandler
  {
    public async Task SendTraceRecord(string level, string message)
    {
      HttpClient client = new HttpClient();

      string key = ConfigurationManager.AppSettings["anubis_key"];
      string application = ConfigurationManager.AppSettings["anubis_application"];

      string url = string.Format("http://localhost:61106/api/tracing/{0}/{1}", key, application);

      LogModel model = new LogModel()
      {
        Level = level,
        Message = message
      };

      try
      {
        HttpResponseMessage response = await client.PostAsJsonAsync<LogModel>(url, model);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
          // What do we do here???
        }
      }
      catch(Exception exp)
      {

      }
    }
  }
}
