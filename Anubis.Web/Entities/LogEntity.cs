using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anubis.Web.Entities
{
    public class LogEntity : TableServiceEntity
    {
        public LogEntity()
        {
            // NO OP for Table Services        
        }

        public LogEntity(string applicationName, long ticks)
        {
            this.PartitionKey = applicationName;
            this.RowKey = String.Format("{0:D19}", DateTime.MaxValue.Ticks - ticks);
        }

        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}