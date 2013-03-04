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

        public LogEntity(int ownerId, string applicationName)
        {
            this.PartitionKey = applicationName;
            this.RowKey = string.Format("{0}-{1}", ownerId, DateTime.UtcNow.Ticks);
        }

        public string LogLevel { get; set; }
        public string Message { get; set; }
    }
}