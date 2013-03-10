using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Web.Models
{
    public class LogModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("stackTrace")]
        public string StackTrace { get; set; }
    }
}