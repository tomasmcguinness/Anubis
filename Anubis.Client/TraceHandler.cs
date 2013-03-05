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
  public class TraceHandler
  {
    public async Task SendTraceRecord(string code, string level, string message)
    {
      HttpClient client = new HttpClient();

      string url = string.Format("http://anubis-we.azurewebsites.net/api/tracing/{0}", code);

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
      catch (Exception exp)
      {

      }
    }
  }
}
